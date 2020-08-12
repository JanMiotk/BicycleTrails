using DirectoriesCreator.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DirectoriesCreator
{
    public class FilePath : IFilePath
    {
        public string Trails { get; set; }
        public string GPXTrails { get; set; }
        public string TrailsAdditionalInformation { get; set; }
        public string DetailedTrails { get; set; }

        public string temp;
        public FilePath()
        {
            Trails = @"..\..\..\..\DownloadResoults\Trails";
            GPXTrails = @"..\..\..\..\DownloadResoults\GPXTrails";
            TrailsAdditionalInformation = @"..\..\..\..\DownloadResoults\TrailsAdditionalInformation";
            DetailedTrails = @"..\..\..\..\DownloadResoults\DetailedTrails";
        } 
    }
}
