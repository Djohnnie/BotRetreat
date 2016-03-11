using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace BotRetreat.ScriptEditor.Wpf
{
    public class HighlighterManager
    {
        private static readonly HighlighterManager instance = new HighlighterManager();
        public static HighlighterManager Instance { get { return instance; } }

        public IDictionary<string, IHighlighter> Highlighters { get; }

        private HighlighterManager()
        {
            Highlighters = new Dictionary<string, IHighlighter>();

            var gfdgd = Application.GetResourceStream(new Uri("pack://application:,,,/BotRetreat.ScriptEditor.Wpf;component/Resources/syntax.xsd"));
            var schemaStream = gfdgd.Stream;
            var schema = XmlSchema.Read(schemaStream, (s, e) => {
                Debug.WriteLine("Xml schema validation error : " + e.Message);
            });

            var readerSettings = new XmlReaderSettings();
            readerSettings.Schemas.Add(schema);
            readerSettings.ValidationType = ValidationType.Schema;

            foreach (var res in GetResources("resources/(.+?)[.]xml"))
            {
                XDocument xmldoc = null;
                try
                {
                    var reader = XmlReader.Create(res.Value, readerSettings);
                    xmldoc = XDocument.Load(reader);
                }
                catch (XmlSchemaValidationException ex)
                {
                    Debug.WriteLine("Xml validation error at line " + ex.LineNumber + " for " + res.Key + " :");
                    Debug.WriteLine("Warning : if you cannot find the issue in the xml file, verify the xsd file.");
                    Debug.WriteLine(ex.Message);
                    return;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return;
                }

                var root = xmldoc.Root;
                var name = root.Attribute("name").Value.Trim();
                Highlighters.Add(name, new XmlHighlighter(root));
            }
        }

        /// <summary>
        /// Returns a dictionary of the assembly resources (not embedded).
        /// Uses a regex filter for the resource paths.
        /// </summary>
        private IDictionary<string, UnmanagedMemoryStream> GetResources(string filter)
        {
            var asm = Assembly.GetCallingAssembly();
            var resName = asm.GetName().Name + ".g.resources";
            var manifestStream = asm.GetManifestResourceStream(resName);
            var reader = new ResourceReader(manifestStream);

            IDictionary<string, UnmanagedMemoryStream> ret = new Dictionary<string, UnmanagedMemoryStream>();
            foreach (DictionaryEntry res in reader)
            {
                var path = (string)res.Key;
                var stream = (UnmanagedMemoryStream)res.Value;
                if (Regex.IsMatch(path, filter))
                    ret.Add(path, stream);
            }
            return ret;
        }

        /// <summary>
        /// An IHighlighter built from an Xml syntax file
        /// </summary>
        private class XmlHighlighter : IHighlighter
        {
            private readonly List<HighlightWordsRule> _wordsRules;
            private readonly List<HighlightLineRule> _lineRules;
            private readonly List<AdvancedHighlightRule> _regexRules;

            public XmlHighlighter(XElement root)
            {
                _wordsRules = new List<HighlightWordsRule>();
                _lineRules = new List<HighlightLineRule>();
                _regexRules = new List<AdvancedHighlightRule>();

                foreach (var elem in root.Elements())
                {
                    switch (elem.Name.ToString())
                    {
                        case "HighlightWordsRule": _wordsRules.Add(new HighlightWordsRule(elem)); break;
                        case "HighlightLineRule": _lineRules.Add(new HighlightLineRule(elem)); break;
                        case "AdvancedHighlightRule": _regexRules.Add(new AdvancedHighlightRule(elem)); break;
                    }
                }
            }

            public int Highlight(FormattedText text, int previousBlockCode)
            {
                //
                // WORDS RULES
                //
                var wordsRgx = new Regex("[a-zA-Z_][a-zA-Z0-9_]*");
                foreach (Match m in wordsRgx.Matches(text.Text))
                {
                    foreach (var rule in _wordsRules)
                    {
                        foreach (var word in rule.Words)
                        {
                            if (rule.Options.IgnoreCase)
                            {
                                if (m.Value.Equals(word, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    text.SetForegroundBrush(rule.Options.Foreground, m.Index, m.Length);
                                    text.SetFontWeight(rule.Options.FontWeight, m.Index, m.Length);
                                    text.SetFontStyle(rule.Options.FontStyle, m.Index, m.Length);
                                }
                            }
                            else {
                                if (m.Value == word)
                                {
                                    text.SetForegroundBrush(rule.Options.Foreground, m.Index, m.Length);
                                    text.SetFontWeight(rule.Options.FontWeight, m.Index, m.Length);
                                    text.SetFontStyle(rule.Options.FontStyle, m.Index, m.Length);
                                }
                            }
                        }
                    }
                }

                //
                // REGEX RULES
                //
                foreach (var rule in _regexRules)
                {
                    var regexRgx = new Regex(rule.Expression);
                    foreach (Match m in regexRgx.Matches(text.Text))
                    {
                        text.SetForegroundBrush(rule.Options.Foreground, m.Index, m.Length);
                        text.SetFontWeight(rule.Options.FontWeight, m.Index, m.Length);
                        text.SetFontStyle(rule.Options.FontStyle, m.Index, m.Length);
                    }
                }

                //
                // LINES RULES
                //
                foreach (var rule in _lineRules)
                {
                    var lineRgx = new Regex(Regex.Escape(rule.LineStart) + ".*");
                    foreach (Match m in lineRgx.Matches(text.Text))
                    {
                        text.SetForegroundBrush(rule.Options.Foreground, m.Index, m.Length);
                        text.SetFontWeight(rule.Options.FontWeight, m.Index, m.Length);
                        text.SetFontStyle(rule.Options.FontStyle, m.Index, m.Length);
                    }
                }

                return -1;
            }
        }

        /// <summary>
        /// A set of words and their RuleOptions.
        /// </summary>
        private class HighlightWordsRule
        {
            public List<string> Words { get; }
            public RuleOptions Options { get; }

            public HighlightWordsRule(XElement rule)
            {
                Words = new List<string>();
                Options = new RuleOptions(rule);

                var wordsStr = rule.Element("Words").Value;
                var words = Regex.Split(wordsStr, "\\s+");

                foreach (var word in words)
                    if (!string.IsNullOrWhiteSpace(word))
                        Words.Add(word.Trim());
            }
        }

        /// <summary>
        /// A line start definition and its RuleOptions.
        /// </summary>
        private class HighlightLineRule
        {
            public string LineStart { get; }
            public RuleOptions Options { get; }

            public HighlightLineRule(XElement rule)
            {
                LineStart = rule.Element("LineStart").Value.Trim();
                Options = new RuleOptions(rule);
            }
        }

        /// <summary>
        /// A regex and its RuleOptions.
        /// </summary>
        private class AdvancedHighlightRule
        {
            public string Expression { get; }
            public RuleOptions Options { get; }

            public AdvancedHighlightRule(XElement rule)
            {
                Expression = rule.Element("Expression").Value.Trim();
                Options = new RuleOptions(rule);
            }
        }

        /// <summary>
        /// A set of options liked to each rule.
        /// </summary>
        private class RuleOptions
        {
            public bool IgnoreCase { get; }
            public Brush Foreground { get; }
            public FontWeight FontWeight { get; }
            public FontStyle FontStyle { get; }

            public RuleOptions(XContainer rule)
            {
                var ignoreCaseStr = rule.Element("IgnoreCase").Value.Trim();
                var foregroundStr = rule.Element("Foreground").Value.Trim();
                var fontWeightStr = rule.Element("FontWeight").Value.Trim();
                var fontStyleStr = rule.Element("FontStyle").Value.Trim();

                IgnoreCase = bool.Parse(ignoreCaseStr);
                Foreground = (Brush)new BrushConverter().ConvertFrom(foregroundStr);
                FontWeight = (FontWeight)new FontWeightConverter().ConvertFrom(fontWeightStr);
                FontStyle = (FontStyle)new FontStyleConverter().ConvertFrom(fontStyleStr);
            }
        }
    }
}