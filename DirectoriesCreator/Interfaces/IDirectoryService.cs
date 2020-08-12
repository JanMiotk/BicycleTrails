
using System.Collections.Generic;

namespace DirectoriesCreator.Interfaces
{
    public interface IDirectoryService
    {
        void CreateDirectories();
        public void RemoveAllDirectories(params string[] directories);
        public List<string> ReturnPathDirectotiesToLoad();
    }
}