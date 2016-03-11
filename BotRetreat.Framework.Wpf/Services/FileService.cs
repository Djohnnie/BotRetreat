using System;
using System.IO;
using System.Threading.Tasks;
using BotRetreat.Framework.Wpf.Services.Interfaces;

namespace BotRetreat.Framework.Wpf.Services
{
    public class FileService : IFileService
    {
        public async Task SaveTextFile(String fileName, String contents)
        {
            using (var streamWriter = new StreamWriter(fileName))
            {
                await streamWriter.WriteAsync(contents);
            }
        }

        public async Task<String> LoadTextFile(String fileName)
        {
            using (var streamReader = new StreamReader(fileName))
            {
                return await streamReader.ReadToEndAsync();
            }
        }
    }
}