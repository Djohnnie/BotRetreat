using System;
using BotRetreat.Framework.Wpf.Services.Interfaces;
using Microsoft.Win32;

namespace BotRetreat.Framework.Wpf.Services
{
    public class FileExplorerService : IFileExplorerService
    {
        public String LoadFile(String filter)
        {
            return File<OpenFileDialog>(filter);
        }

        public String SaveFile(String filter)
        {
            return File<SaveFileDialog>(filter);
        }

        private static String File<TFileDialog>(String filter) where TFileDialog : FileDialog, new()
        {
            var dialog = new TFileDialog { Filter = filter };
            if (dialog.ShowDialog() ?? false)
            {
                return dialog.FileName;
            }
            return null;
        }
    }
}