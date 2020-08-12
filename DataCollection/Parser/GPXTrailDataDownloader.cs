using DirectoriesCreator.Interfaces;
using Models;
using Parser.Interfaces;
using Serializer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Parser.Parser
{
    public class GPXTrailDataDownloader : IGPXTrailDataDownloader
    {
        private readonly ISerializer _serializer;
        private readonly IFilePath _filePath;
        public GPXTrailDataDownloader(ISerializer serializer, IFilePath filePath)
        {
            _serializer = serializer;
            _filePath = filePath;
        }
        public void DownloadData()
        {
            for (int i = 1; i <= new DirectoryInfo(_filePath.Trails).GetFiles().Length; i++)
            {
                List<Trail> trails;

                try
                {
                    trails = _serializer.Deserialize<Trail>(i, _filePath.Trails, "ListOfTrails");
                }
                catch
                {
                    continue;
                }

                foreach (var trail in trails)
                {
                    var path = trail.Link.Split("/")[4];
                    DownloadGpx(path, _filePath.GPXTrails);
                }
                trails = null;
            }
        }
        public void DownloadGpx(string path, string PathForGpxTrails)
        {
            Cookie C = new Cookie("PHPSESSID",
                "psid6u33c2cv1vk232e0a9snm5",
                   "/");

            using (WebClient Client = new WebClient())
            {
                Client.Headers.Add(HttpRequestHeader.Cookie, $"{C}");
                Client.DownloadFile($"https://www.traseo.pl/trasa-pobierz/{path}", $@"{PathForGpxTrails}\{path}.gpx");
            }
        }
    }
}
