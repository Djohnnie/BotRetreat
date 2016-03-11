using System;

namespace BotRetreat.Framework.Wpf.Services.Interfaces
{
    public interface IFileExplorerService
    {
        String LoadFile(String filter);

        String SaveFile(String filter);
    }
}