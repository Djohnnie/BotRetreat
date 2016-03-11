using System.ComponentModel;

namespace BotRetreat.Enums
{
    public enum HistoryName
    {
        [Description("No hisotry")]
        Empty,

        [Description("Bot {0} has been deployed")]
        BotDeployed,

        [Description("Bot script has stated")]
        BotScriptStarted,

        [Description("Bot script has finished")]
        BotScriptFinished,

        [Description("Bot script has thrown exception")]
        BotScriptError,

        [Description("Bot has moved north")]
        BotMovingNorth,

        [Description("Bot has moved east")]
        BotMovingEast,

        [Description("Bot has moved south")]
        BotMovingSouth,

        [Description("Bot has moved west")]
        BotMovingWest,

        [Description("Bot has turned left")]
        BotTurningLeft,

        [Description("Bot has turned right")]
        BotTurningRight,

        [Description("Bot has turned around")]
        BotTurningAround,

        [Description("Bot has executed a melee backstab attack")]
        BotMeleeBackstabAttack,

        [Description("Bot has executed a melee attack")]
        BotMeleeAttack,

        [Description("Bot has executed a ranged attack")]
        BotRangedAttack,

        [Description("Bot has executed a teleportation trick")]
        BotTeleport
    }
}