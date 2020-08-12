using DirectoriesCreator.Interfaces;
using Models;
using Parser.Interfaces;
using Serializer.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace DownloadDataFromTraseo.FileOperations
{
    public class FileCombining : IFileCombining
    {
        private readonly ISerializer _serializer;

        private readonly IFilePath _filePath;

        private readonly IDirectoryService _directoryService;

        public FileCombining(ISerializer serializer, IFilePath filePath, IDirectoryService directoryService)
        {
            _serializer = serializer;
            _filePath = filePath;
            _directoryService = directoryService;
        }
        public void CombineFiles()
        {
            for (int i = 1; i <= new DirectoryInfo(_filePath.Trails).GetFiles().Length; i++)
            {
                List<DetailedTrail> detailedTrails = new List<DetailedTrail>();
                List<AdditionalInfo> AdditionalInfo;
                List<Trail> bicycleTrails;

                try
                {
                    bicycleTrails = _serializer.Deserialize<Trail>(i, _filePath.Trails, "ListOfTrails");
                }
                catch
                {
                    continue;
                }
                try
                {
                    AdditionalInfo = _serializer.Deserialize<AdditionalInfo>(i, _filePath.TrailsAdditionalInformation, "ListOfTrails");
                }
                catch
                {
                    continue;
                }
                for (int j = 0; j < bicycleTrails.Count; j++)
                {
                    Trail trail = bicycleTrails[j];
                    AdditionalInfo additionalInfo = AdditionalInfo[j];
                    XmlDocument gpxFile;
                    try
                    {
                        gpxFile = _serializer.DeserializeXmlDocument(_filePath.GPXTrails, trail.Link.Split(@"https://www.traseo.pl/trasa/")[1]);
                    }
                    catch
                    {

                        continue;
                    }
                    try
                    {
                        detailedTrails.Add(new DetailedTrail
                        {
                            Title = trail.Title,
                            Rating = trail.Rating,
                            Distance = trail.Distance,
                            Duration = trail.Duration,
                            Photo = trail.Photo,
                            Map = trail.Map,
                            Author = trail.Author,
                            DifficultyLevel = trail.DifficultyLevel,
                            Link = trail.Link,
                            Trail = gpxFile.InnerXml,
                            Location = additionalInfo.Location,
                            Description = additionalInfo.Description,
                            KindOfActivity = additionalInfo.KindOfActivity,
                            AverageSpeed = additionalInfo.AverageSpeed,
                            Exceedance = additionalInfo.Exceedance,
                            SumUp = additionalInfo.SumUp,
                            SumDown = additionalInfo.SumDown,
                            Data = additionalInfo.Data
                        });
                    }
                    catch
                    {
                        continue;
                    }
                }
                _serializer.Serialize<DetailedTrail>(ref detailedTrails, _filePath.DetailedTrails, i);
            }
            _directoryService.RemoveAllDirectories(_filePath.Trails, _filePath.GPXTrails, _filePath.TrailsAdditionalInformation);
        }
    }
}
