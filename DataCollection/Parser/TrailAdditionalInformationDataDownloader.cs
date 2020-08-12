using DirectoriesCreator.Interfaces;
using DownloadDataFromTraseo.Interfaces;
using HtmlAgilityPack;
using Models;
using Serializer.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace DownloadDataFromTraseo.Parser
{
    public class TrailAdditionalInformationDataDownloader : ITrailAdditionalInformationDataDownloader
    {
        private HtmlDocument document { get; set; }
        private ISerializer _serializer { get; set; }

        private readonly string _pathToBicycleTrails;
        private readonly string _pathToAdditionalInformations;
        private readonly IFilePath _filePath;


        public TrailAdditionalInformationDataDownloader(ISerializer serializer, IFilePath filePath)
        {
            _serializer = serializer;
            _filePath = filePath;
            document = new HtmlDocument();
        }
        public HtmlDocument ReturnPage(string url)
        {
            using (var client = new WebClient())
            {
                var content = client.DownloadString(url);

                document.LoadHtml(content);

                return document;
            }

        }
        public string ReturnKindOfActivity(HtmlDocument document)
        {
            return document?.DocumentNode?.SelectSingleNode("//span[@class=\"kind-of-act\"]")?.InnerText;
        }
        public string ReturnDifficulty(HtmlDocument document)
        {
            return document?.DocumentNode?.SelectSingleNode("//span[@class=\"difficulty \"]")?.InnerText;
        }
        public string ReturnRating(HtmlDocument document)
        {
            return document?.DocumentNode?.SelectSingleNode("//span[@class=\"stars\"]")?.InnerText;
        }
        public string ReturnDistance(HtmlDocument document)
        {
            return document?.DocumentNode?.SelectSingleNode("//span[@class=\"distance\"]")?.InnerText;
        }
        public string ReturnDuration(HtmlDocument document)
        {
            return document?.DocumentNode?.SelectSingleNode("//span[@class=\"duration\"]")?.InnerText;
        }
        public string ReturnAverageSpeed(HtmlDocument document)
        {
            return document?.DocumentNode?.SelectSingleNode("//span[@class=\"average-speed\"]")?.InnerText;
        }
        public string ReturnExceeding(HtmlDocument document)
        {
            return document?.DocumentNode?.SelectNodes("//span[@class=\"exceeding \"]")?[0].InnerText;
        }
        public string ReturnSumUp(HtmlDocument document)
        {
            return document?.DocumentNode?.SelectNodes("//span[@class=\"exceeding \"]")?[1].InnerText;
        }
        public string ReturnSumDown(HtmlDocument document)
        {
            return document?.DocumentNode?.SelectNodes("//span[@class=\"exceeding \"]")?[2].InnerText;
        }
        public string ReturnData(HtmlDocument document)
        {
            return document?.DocumentNode?.SelectSingleNode("//span[@class=\"tradd-date\"]")?.InnerText;
        }
        public string ReturnLocation(HtmlDocument document)
        {
            return document?.DocumentNode?.SelectSingleNode("//span[@class=\"location\"]")?.InnerText;
        }
        public string ReturnDescription(HtmlDocument document)
        {
            return document?.DocumentNode?.SelectSingleNode("//div[@class=\"text-content route-detail-description\"]")?.InnerText;
        }
        public void DownloadData()
        {
            for (int i = 1; i <= new DirectoryInfo(_filePath.Trails).GetFiles().Length; i++)
            {
                List<AdditionalInfo> aditionalInfo = new List<AdditionalInfo>();
                List<Trail> bicycleTrails;
                try
                {
                    bicycleTrails = _serializer.Deserialize<Trail>(i, _filePath.Trails, "ListOfTrails");
                }
                catch
                {
                    continue;
                }
                foreach (var trail in bicycleTrails)
                {
                    AdditionalInfo information = new AdditionalInfo();
                    var document = ReturnPage(trail.Link);

                    information.KindOfActivity = ReturnKindOfActivity(document);
                    information.DifficultyLevel = ReturnDifficulty(document);
                    information.Rating = ReturnRating(document);
                    information.Distance = ReturnDistance(document);
                    information.Duration = ReturnDuration(document);
                    information.AverageSpeed = ReturnAverageSpeed(document);
                    information.Exceedance = ReturnExceeding(document);
                    information.SumUp = ReturnSumUp(document);
                    information.SumDown = ReturnSumDown(document);
                    information.Data = ReturnData(document);
                    information.Location = ReturnLocation(document);
                    information.Description = ReturnDescription(document);

                    aditionalInfo.Add(information);
                }
                _serializer.Serialize<AdditionalInfo>(ref aditionalInfo, _filePath.TrailsAdditionalInformation, i);
            }

        }
    }
}
