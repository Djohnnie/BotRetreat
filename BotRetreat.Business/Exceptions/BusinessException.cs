using System;

namespace BotRetreat.Business.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(String message, Exception innerException = null) : base(message, innerException) { }
    }
}