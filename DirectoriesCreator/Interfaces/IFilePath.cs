using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoriesCreator.Interfaces
{
    public interface IFilePath
    {
        string Trails { get; set; }
        string GPXTrails { get; set; }
        string TrailsAdditionalInformation { get; set; }
        string DetailedTrails { get; set; }
    }
}
