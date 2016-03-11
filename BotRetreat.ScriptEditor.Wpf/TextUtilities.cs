using System;

namespace BotRetreat.ScriptEditor.Wpf
{
    public static class TextExtensions
    {
        /// <summary>
        /// Returns the raw number of the current line count.
        /// </summary>
        public static Int32 GetLineCount(this String text)
        {
            int lcnt = 1;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '\n')
                    lcnt += 1;
            }
            return lcnt;
        }

        /// <summary>
        /// Returns the index of the first character of the
        /// specified line. If the index is greater than the current
        /// line count, the method returns the index of the last
        /// character. The line index is zero-based.
        /// </summary>
        public static Int32 GetFirstCharIndexFromLineIndex(this String text, Int32 lineIndex)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            if (lineIndex <= 0)
                return 0;

            int currentLineIndex = 0;
            for (int i = 0; i < text.Length - 1; i++)
            {
                if (text[i] == '\n')
                {
                    currentLineIndex += 1;
                    if (currentLineIndex == lineIndex)
                        return Math.Min(i + 1, text.Length - 1);
                }
            }

            return Math.Max(text.Length - 1, 0);
        }

        /// <summary>
        /// Returns the index of the last character of the
        /// specified line. If the index is greater than the current
        /// line count, the method returns the index of the last
        /// character. The line-index is zero-based.
        /// </summary>
        public static Int32 GetLastCharIndexFromLineIndex(this String text, Int32 lineIndex)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            if (lineIndex < 0)
                return 0;

            int currentLineIndex = 0;
            for (int i = 0; i < text.Length - 1; i++)
            {
                if (text[i] == '\n')
                {
                    if (currentLineIndex == lineIndex)
                        return i;
                    currentLineIndex += 1;
                }
            }

            return Math.Max(text.Length - 1, 0);
        }
    }
}