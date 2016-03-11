using System;

namespace BotRetreat.DataTransferObjects
{
    public class ScriptValidationMessage : IDataTransferObject
    {
        public String Message { get; set; }

        public Int32 LocationStart { get; set; }

        public Int32 LocationEnd { get; set; }
    }
}