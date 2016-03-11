using System;

namespace BotRetreat.Client.Exceptions
{
    public class ClientException : Exception
    {
        public ClientException(String message) : base(message) { }
    }
}