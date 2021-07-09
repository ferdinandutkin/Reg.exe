using System;

namespace CW.Data
{
    class ServerInteractionSigleton
    {
        //https://localhost:5001/api/
        //https://regexe.azurewebsites.net/api/
        private static readonly
                Lazy<ConsoleClient.ServerInteraction> lazy
                = new(
                    () => new ConsoleClient.ServerInteraction("https://regexe.azurewebsites.net/api/"));
        public static ConsoleClient.ServerInteraction Instance => lazy.Value;

    }
}
