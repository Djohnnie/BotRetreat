using System;
using System.Windows.Media;

namespace BotRetreat.ScriptEditor.Wpf
{
    public interface IHighlighter
    {
        /// <summary>
        /// Highlights the text of the current block.
        /// </summary>
        /// <param name="text">The text from the current block to highlight</param>
        /// <param name="previousBlockCode">The code assigned to the previous block, or -1 if
        /// there is no previous block</param>
        /// <returns>The current block code</returns>
        Int32 Highlight(FormattedText text, Int32 previousBlockCode);
    }
}