using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace BotRetreat.ScriptEditor.Wpf
{
    /// <summary>
    /// Interaction logic for ScriptEditor.xaml
    /// </summary>
    public partial class ScriptEditor : TextBox
    {
        // --------------------------------------------------------------------
        // Attributes
        // --------------------------------------------------------------------

        public double LineHeight
        {
            get { return _lineHeight; }
            set
            {
                if (value != _lineHeight)
                {
                    _lineHeight = value;
                    _blockHeight = MaxLineCountInBlock * value;
                    TextBlock.SetLineStackingStrategy(this, LineStackingStrategy.BlockLineHeight);
                    TextBlock.SetLineHeight(this, _lineHeight);
                }
            }
        }

        public int MaxLineCountInBlock
        {
            get { return _maxLineCountInBlock; }
            set
            {
                _maxLineCountInBlock = value > 0 ? value : 0;
                _blockHeight = value * LineHeight;
            }
        }

        public IHighlighter CurrentHighlighter { get; set; }

        private DrawingControl _renderCanvas;
        private DrawingControl _lineNumbersCanvas;
        private double _lineHeight;
        private int _totalLineCount;
        private readonly List<InnerTextBlock> _blocks;
        private double _blockHeight;
        private int _maxLineCountInBlock;

        // --------------------------------------------------------------------
        // Ctor and event handlers
        // --------------------------------------------------------------------

        public ScriptEditor()
        {
            InitializeComponent();

            MaxLineCountInBlock = 100;
            LineHeight = FontSize * 1.3;
            _totalLineCount = 1;
            _blocks = new List<InnerTextBlock>();

            Loaded += (s, e) =>
            {
                _renderCanvas = (DrawingControl)Template.FindName("PART_RenderCanvas", this);
                _lineNumbersCanvas = (DrawingControl)Template.FindName("PART_LineNumbersCanvas", this);
                var scrollViewer = (ScrollViewer)Template.FindName("PART_ContentHost", this);

                if (_lineNumbersCanvas != null)
                {
                    _lineNumbersCanvas.Width = GetFormattedTextWidth($"{_totalLineCount:0000}") + 5;
                }

                if (scrollViewer != null)
                {
                    scrollViewer.ScrollChanged += OnScrollChanged;
                }

                InvalidateBlocks(0);
                InvalidateVisual();
            };

            SizeChanged += (s, e) =>
            {
                if (e.HeightChanged == false)
                    return;
                UpdateBlocks();
                InvalidateVisual();
            };

            TextChanged += (s, e) =>
            {
                UpdateTotalLineCount();
                InvalidateBlocks(e.Changes.First().Offset);
                InvalidateVisual();
            };

            CurrentHighlighter = HighlighterManager.Instance.Highlighters["C#"];
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            DrawBlocks();
            base.OnRender(drawingContext);
        }

        private void OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalChange != 0)
                UpdateBlocks();
            InvalidateVisual();
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            e.Handled = true;
            Binding binding = BindingOperations.GetBinding(this, TextProperty);
            if (binding.UpdateSourceTrigger == UpdateSourceTrigger.Default ||
                binding.UpdateSourceTrigger == UpdateSourceTrigger.LostFocus)
            {
                BindingOperations.GetBindingExpression(this, TextProperty).UpdateSource();
            }
        }

        // -----------------------------------------------------------
        // Updating & Block managing
        // -----------------------------------------------------------

        private void UpdateTotalLineCount()
        {
            _totalLineCount = Text.GetLineCount();
        }

        private void UpdateBlocks()
        {
            if (_blocks.Count == 0)
                return;

            // While something is visible after last block...
            while (!_blocks.Last().IsLast && _blocks.Last().Position.Y + _blockHeight - VerticalOffset < ActualHeight)
            {
                var firstLineIndex = _blocks.Last().LineEndIndex + 1;
                var lastLineIndex = firstLineIndex + _maxLineCountInBlock - 1;
                lastLineIndex = lastLineIndex <= _totalLineCount - 1 ? lastLineIndex : _totalLineCount - 1;

                var fisrCharIndex = _blocks.Last().CharEndIndex + 1;
                var lastCharIndex = Text.GetLastCharIndexFromLineIndex(lastLineIndex); // to be optimized (forward search)

                if (lastCharIndex <= fisrCharIndex)
                {
                    _blocks.Last().IsLast = true;
                    return;
                }

                var block = new InnerTextBlock(
                    fisrCharIndex,
                    lastCharIndex,
                    _blocks.Last().LineEndIndex + 1,
                    lastLineIndex,
                    LineHeight);
                block.RawText = block.GetSubString(Text);
                block.LineNumbers = GetFormattedLineNumbers(block.LineStartIndex, block.LineEndIndex);
                _blocks.Add(block);
                FormatBlock(block, _blocks.Count > 1 ? _blocks[_blocks.Count - 2] : null);
            }
        }

        private void InvalidateBlocks(int changeOffset)
        {
            InnerTextBlock blockChanged = null;
            for (var i = 0; i < _blocks.Count; i++)
            {
                if (_blocks[i].CharStartIndex <= changeOffset && changeOffset <= _blocks[i].CharEndIndex + 1)
                {
                    blockChanged = _blocks[i];
                    break;
                }
            }

            if (blockChanged == null && changeOffset > 0)
                blockChanged = _blocks.Last();

            var fvline = blockChanged?.LineStartIndex ?? 0;
            var lvline = GetIndexOfLastVisibleLine();
            var fvchar = blockChanged?.CharStartIndex ?? 0;
            var lvchar = Text.GetLastCharIndexFromLineIndex(lvline);

            if (blockChanged != null)
                _blocks.RemoveRange(_blocks.IndexOf(blockChanged), _blocks.Count - _blocks.IndexOf(blockChanged));

            var localLineCount = 1;
            var charStart = fvchar;
            var lineStart = fvline;
            for (var i = fvchar; i < Text.Length; i++)
            {
                if (Text[i] == '\n')
                {
                    localLineCount += 1;
                }
                if (i == Text.Length - 1)
                {
                    var blockText = Text.Substring(charStart);
                    var block = new InnerTextBlock(
                        charStart,
                        i, lineStart,
                        lineStart + blockText.GetLineCount() - 1,
                        LineHeight);
                    block.RawText = block.GetSubString(Text);
                    block.LineNumbers = GetFormattedLineNumbers(block.LineStartIndex, block.LineEndIndex);
                    block.IsLast = true;

                    if (_blocks.Any(b => b.LineStartIndex == block.LineStartIndex))
                    {
                        throw new Exception();
                    }

                    _blocks.Add(block);
                    FormatBlock(block, _blocks.Count > 1 ? _blocks[_blocks.Count - 2] : null);
                    break;
                }
                if (localLineCount > _maxLineCountInBlock)
                {
                    var block = new InnerTextBlock(
                        charStart,
                        i,
                        lineStart,
                        lineStart + _maxLineCountInBlock - 1,
                        LineHeight);
                    block.RawText = block.GetSubString(Text);
                    block.LineNumbers = GetFormattedLineNumbers(block.LineStartIndex, block.LineEndIndex);

                    if (_blocks.Any(b => b.LineStartIndex == block.LineStartIndex))
                    {
                        throw new Exception();
                    }

                    _blocks.Add(block);
                    FormatBlock(block, _blocks.Count > 1 ? _blocks[_blocks.Count - 2] : null);

                    charStart = i + 1;
                    lineStart += _maxLineCountInBlock;
                    localLineCount = 1;

                    if (i > lvchar)
                        break;
                }
            }
        }

        // -----------------------------------------------------------
        // Rendering
        // -----------------------------------------------------------

        private void DrawBlocks()
        {
            if (!IsLoaded || _renderCanvas == null || _lineNumbersCanvas == null)
                return;

            var dc = _renderCanvas.GetContext();
            var dc2 = _lineNumbersCanvas.GetContext();
            foreach (var block in _blocks)
            {
                var blockPos = block.Position;
                var top = blockPos.Y - VerticalOffset;
                var bottom = top + _blockHeight;
                if (top < ActualHeight && bottom > 0)
                {
                    try
                    {
                        dc.DrawText(block.FormattedText, new Point(2 - HorizontalOffset, block.Position.Y - VerticalOffset));
                        if (IsLineNumbersMarginVisible)
                        {
                            _lineNumbersCanvas.Width = GetFormattedTextWidth($"{_totalLineCount:0000}") + 5;
                            dc2.DrawText(block.LineNumbers, new Point(_lineNumbersCanvas.ActualWidth, 1 + block.Position.Y - VerticalOffset));
                        }
                    }
                    catch
                    {
                        // Don't know why this exception is raised sometimes.
                        // Reproduce steps:
                        // - Sets a valid syntax highlighter on the box.
                        // - Copy a large chunk of code in the clipboard.
                        // - Paste it using ctrl+v and keep these buttons pressed.
                    }
                }
            }
            dc.Close();
            dc2.Close();
        }

        // -----------------------------------------------------------
        // Utilities
        // -----------------------------------------------------------

        /// <summary>
        /// Returns the index of the first visible text line.
        /// </summary>
        public int GetIndexOfFirstVisibleLine()
        {
            var guessedLine = (int)(VerticalOffset / _lineHeight);
            return guessedLine > _totalLineCount ? _totalLineCount : guessedLine;
        }

        /// <summary>
        /// Returns the index of the last visible text line.
        /// </summary>
        public int GetIndexOfLastVisibleLine()
        {
            var height = VerticalOffset + ViewportHeight;
            var guessedLine = (int)(height / _lineHeight);
            return guessedLine > _totalLineCount - 1 ? _totalLineCount - 1 : guessedLine;
        }

        /// <summary>
        /// Formats and Highlights the text of a block.
        /// </summary>
        private void FormatBlock(InnerTextBlock currentBlock, InnerTextBlock previousBlock)
        {
            currentBlock.FormattedText = GetFormattedText(currentBlock.RawText);
            if (CurrentHighlighter != null)
            {
                var previousCode = previousBlock?.Code ?? -1;
                currentBlock.Code = CurrentHighlighter.Highlight(currentBlock.FormattedText, previousCode);
            }
        }

        /// <summary>
        /// Returns a formatted text object from the given string
        /// </summary>
        private FormattedText GetFormattedText(string text)
        {
            var ft = new FormattedText(
                text,
                CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
                FontSize,
                Brushes.Black)
            {
                Trimming = TextTrimming.None,
                LineHeight = _lineHeight
            };


            return ft;
        }

        /// <summary>
        /// Returns a string containing a list of numbers separated with newlines.
        /// </summary>
        private FormattedText GetFormattedLineNumbers(int firstIndex, int lastIndex)
        {
            var text = "";
            for (var i = firstIndex + 1; i <= lastIndex + 1; i++)
                text += i.ToString() + "\n";
            text = text.Trim();

            var ft = new FormattedText(
                text,
                CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
                FontSize,
                new SolidColorBrush(Color.FromRgb(0x21, 0xA1, 0xD8)))
            {
                Trimming = TextTrimming.None,
                LineHeight = _lineHeight,
                TextAlignment = TextAlignment.Right
            };


            return ft;
        }

        /// <summary>
        /// Returns the width of a text once formatted.
        /// </summary>
        private double GetFormattedTextWidth(string text)
        {
            var ft = new FormattedText(
                text,
                CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                new Typeface(FontFamily, FontStyle, FontWeight, FontStretch),
                FontSize,
                Brushes.Black)
            {
                Trimming = TextTrimming.None,
                LineHeight = _lineHeight
            };


            return ft.Width;
        }

        // -----------------------------------------------------------
        // Dependency Properties
        // -----------------------------------------------------------

        public static readonly DependencyProperty IsLineNumbersMarginVisibleProperty = DependencyProperty.Register(
            "IsLineNumbersMarginVisible", typeof(bool), typeof(ScriptEditor), new PropertyMetadata(true));

        public bool IsLineNumbersMarginVisible
        {
            get { return (bool)GetValue(IsLineNumbersMarginVisibleProperty); }
            set { SetValue(IsLineNumbersMarginVisibleProperty, value); }
        }

        public static readonly DependencyProperty SelectionProperty = DependencyProperty.Register(
           "Selection", typeof(Point), typeof(ScriptEditor), new PropertyMetadata(SelectionHasChanged));

        private static void SelectionHasChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ScriptEditor)d).Select((Int32)((Point)e.NewValue).X, (Int32)((Point)e.NewValue).Y);
        }

        public Point Selection
        {
            get { return (Point)GetValue(SelectionProperty); }
            set
            {
                SetValue(SelectionProperty, value);
            }
        }

        // -----------------------------------------------------------
        // Classes
        // -----------------------------------------------------------

        private class InnerTextBlock
        {
            public string RawText { get; set; }
            public FormattedText FormattedText { get; set; }
            public FormattedText LineNumbers { get; set; }
            public int CharStartIndex { get; private set; }
            public int CharEndIndex { get; private set; }
            public int LineStartIndex { get; private set; }
            public int LineEndIndex { get; private set; }
            public Point Position => new Point(0, LineStartIndex * _lineHeight);
            public bool IsLast { get; set; }
            public int Code { get; set; }

            private readonly double _lineHeight;

            public InnerTextBlock(int charStart, int charEnd, int lineStart, int lineEnd, double lineHeight)
            {
                CharStartIndex = charStart;
                CharEndIndex = charEnd;
                LineStartIndex = lineStart;
                LineEndIndex = lineEnd;
                _lineHeight = lineHeight;
                IsLast = false;

            }

            public string GetSubString(string text)
            {
                return text.Substring(CharStartIndex, CharEndIndex - CharStartIndex + 1);
            }

            public override string ToString()
            {
                return $"L:{LineStartIndex}/{LineEndIndex} C:{CharStartIndex}/{CharEndIndex} {FormattedText.Text}";
            }
        }
    }
}