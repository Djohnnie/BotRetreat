using System;

namespace BotRetreat.Enums
{
    public static class EnumExtensions
    {
        public static String GetName(this HistoryName historyName)
        {
            return historyName.ToString();
        }

        public static String GetDescription(this HistoryName historyName)
        {
            return String.Empty;
        }
    }
}