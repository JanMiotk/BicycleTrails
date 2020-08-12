using DirectoriesCreator.Interfaces;
using DownloadDataFromTraseo.Interfaces;
using Parser.Interfaces;

namespace DownloadDataFromTraseo.Interfaces
{
    public interface IProgram
    {
        public void Run(IDirectoryService directoryService, ITrailDataDownloader trailDataDownloader, IGPXTrailDataDownloader gPXTrailDataDownloader
            , ITrailAdditionalInformationDataDownloader trailAdditionalInformationDataDownloader, IFileCombining fileCombining);
    }
}