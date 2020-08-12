using DirectoriesCreator.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DirectoriesCreator
{
    public class DirectoryService : IDirectoryService
    {
        private readonly IFilePath _filePath;
        public DirectoryService(IFilePath filePath)
        {
            _filePath = filePath;
        }
        public void CreateDirectories()
        {
            foreach (var prop in _filePath.GetType().GetProperties())
            {
                Directory.CreateDirectory(prop.GetValue(_filePath, null).ToString());
            }
        }

        public void RemoveAllDirectories(params string[] directories)
        {
            foreach (var directory in directories)
            {
                DirectoryInfo dir = new DirectoryInfo(directory);

                foreach (var file in dir.EnumerateFiles())
                {
                    file.Delete();
                }

                dir.Delete(true);
            }
        }

        public List<string> ReturnPathDirectotiesToLoad()
        {
            List<string> paths = new List<string>();

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            foreach (var prop in _filePath.GetType().GetProperties())
            {
                var path = Path.GetFullPath(Path.Combine(baseDirectory, prop.GetValue(_filePath, null).ToString()));

                if (Directory.Exists($@"{path}"))
                {
                    paths.Add(path);
                }
            }
            return paths;
        }
    }
}
