using System;
using System.Collections.Generic;

namespace BotRetreat.DataTransferObjects
{
    public class ScriptValidation : IDataTransferObject
    {
        public String Script { get; set; }

        public Int64 CompilationTimeInMilliseconds { get; set; }

        public Int64 RunTimeInMilliseconds { get; set; }

        public List<ScriptValidationMessage> Messages { get; set; }
    }
}