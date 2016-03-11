using System;

namespace BotRetreat.Routes
{
    public static class RouteConstants
    {
        public const String PREFIX = "api";

        public const String GET_HISTORY_BY_ARENA = "arena/{arenaName}/history/{fromDateTime?}/{untilDateTime?}";
        public const String GET_HISTORY_BY_BOT = "bot/{botName}/history/{fromDateTime?}/{untilDateTime?}";

        public const String GET_ARENAS = "arenas";
        public const String GET_ARENAS_LIST = "arenas/list";
        public const String GET_AVAILABLE_ARENAS = "arenas/available";
        public const String GET_TEAM_ARENA = "arenas/team/{teamName}/{teamPassword}";
        public const String GET_TEAM_ARENAS = "arenas/team/available/{teamName}/{teamPassword}";
        public const String POST_ARENA = "arenas";
        public const String PUT_ARENA = "arenas";
        public const String DELETE_ARENA = "arenas/{id}";

        public const String GET_TEAMS = "teams";
        public const String GET_TEAM = "teams/{name}/{password}";
        public const String POST_TEAM = "teams";
        public const String PUT_TEAM = "teams";
        public const String DELETE_TEAM = "teams/{id}";

        public const String GET_BOTS = "bot";
        public const String POST_BOT = "bot";
        public const String PUT_BOT = "bot";
        public const String DELETE_BOT = "bot";

        public const String POST_DEPLOYMENT = "deployment";
        public const String POST_DEPLOYMENT_AVAILABLE = "deployment/available/{teamName}/{arenaName}";

        public const String GET_GAME = "game/arena/{arenaName}";

        public const String POST_SCRIPT_VALIDATION = "script/validate";

        public const String GET_STATISTICS_TEAM = "statistics/team/{teamName}/{teamPassword}";
        public const String GET_STATISTICS_BOT = "statistics/bot/{teamName}/{teamPassword}/{arenaName}";
    }
}