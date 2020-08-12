using Parser.Interfaces;

namespace Parser.Interfaces
{
    public interface IGPXTrailDataDownloader : IDataDownloader
    {
        void DownloadGpx(string path, string PathForGpxTrails);
    }
}