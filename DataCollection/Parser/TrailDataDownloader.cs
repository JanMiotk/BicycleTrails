using DirectoriesCreator.Interfaces;
using HtmlAgilityPack;
using Models;
using Parser.Interfaces;
using Serializer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Parser.Parser
{
    public class TrailDataDownloader : ITrailDataDownloader
    {
        private string url;
        private HtmlDocument document;
        private readonly ISerializer _serializer;
        private readonly IFilePath _filePath;

        public TrailDataDownloader(ISerializer serializer, IFilePath filePath)
        {
            url = @$"https://www.traseo.pl/trasy/score/4/kategoria/rowerowe/";
            document = new HtmlDocument();
            _serializer = serializer;
            _filePath = filePath;
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
        public int ReturnQuantityOfPages(HtmlDocument htmlbody)
        {
            var pagination = htmlbody.DocumentNode.SelectSingleNode("//ul[@class='ti-pagination ti-flat']");
            var allElements = pagination.SelectNodes("li").ToList();
            int last = allElements.Count() - 1;
            var linkToLastPage = allElements[last].InnerHtml;
            var pattern = new Regex(@"<a.+?href=(.)/(\w+)/(\w+)/(\d)/(\w+)/(\w+)/(\w+)/(\d+)(.)>&gt;&gt;</a>");
            Match returnMatch = pattern.Match(linkToLastPage);
            return Convert.ToInt32(returnMatch.Groups[8].Value);
        }
        public HtmlNode ReturnPartOfRow(HtmlNode line, int index)
        {
            return line.Elements("div").ToList()[index];
        }
        public string ReturnTitle(HtmlNode line)
        {
            var title = ReturnPartOfRow(line, 0).Element("h3").Element("a").InnerText.Trim();
            return title;
        }
        public string ReturnDataFromInfoNode(HtmlNode line, int index)
        {
            return ReturnPartOfRow(line, 2).Element("div").Elements("div").ToList()[index].InnerText;
        }
        public double ReturnRating(HtmlNode line)
        {
            var rating = ReturnDataFromInfoNode(line, 1).Replace(".", ",");
            return Convert.ToDouble(rating);
        }
        public string ReturnDistance(HtmlNode line)
        {
            return ReturnDataFromInfoNode(line, 2);
        }
        public string ReturnDuration(HtmlNode line)
        {
            return ReturnDataFromInfoNode(line, 3);
        }
        public byte[] DownloadPhoto(string url)
        {
            using (var client = new WebClient())
            {
                var photo = client.DownloadData(url);
                return photo;
            }
        }
        public byte[] ReturnPicture(HtmlNode line, string url, int index)
        {
            var element = ReturnPartOfRow(line, 2).Elements("div").ToList()[index].Element("a");
            var linkToPhoto = element.Attributes["style"].Value.Split("url")[1]
                .Replace("'", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty);

            return DownloadPhoto($"{url}{linkToPhoto}");

        }
        public string ReturnAuthor(HtmlNode line)
        {
            return ReturnPartOfRow(line, 4)?.InnerText.Replace("\n", string.Empty).Replace("\t", string.Empty).Replace("\r", string.Empty).Trim();
        }
        public string ReturnDificulty(HtmlNode line)
        {
            return ReturnPartOfRow(line, 5).Element("span")?.Attributes["title"].Value.Trim();
        }
        public string ReturnLocation(HtmlNode line)
        {
            return ReturnPartOfRow(line, 5).Element("a") != null ? ReturnPartOfRow(line, 5).Element("a").InnerText.Trim() : null;
        }
        public string ReturnLink(HtmlNode line)
        {
            var element = ReturnPartOfRow(line, 2).Elements("div").ToList()[2].Element("a").Attributes["href"].Value;
            return $@"https://www.traseo.pl{element}";
        }
        public Trail AddToList(HtmlNode line)
        {
            return new Trail
            {
                Title = ReturnTitle(line),
                Rating = ReturnRating(line),
                Distance = ReturnDistance(line),
                Duration = ReturnDuration(line),
                Photo = ReturnPicture(line, @"https://www.traseo.pl/", 1),
                Map = ReturnPicture(line, @"https://www.traseo.pl/", 2),
                Author = ReturnAuthor(line),
                DifficultyLevel = ReturnDificulty(line),
                Location = ReturnLocation(line),
                Link = ReturnLink(line)

            };
        }

        public void DownloadData()
        {
            int quantityOfPages = ReturnQuantityOfPages(ReturnPage(url));

            for (int i = 1; i <= quantityOfPages; i++)
            {
                List<Trail> TrailsFromTraseo = new List<Trail>();
                HtmlNode htmlbody = ReturnPage($"{url}page/{i}").DocumentNode.SelectSingleNode("//body");
                var listOfTrails = htmlbody.SelectSingleNode("//div[@class='routes-list']");
                try
                {
                    foreach (var line in listOfTrails.SelectNodes("div[@class='route-list-view']"))
                    {
                        TrailsFromTraseo.Add(AddToList(line));
                    }
                    _serializer.Serialize<Trail>(ref TrailsFromTraseo, _filePath.Trails, i);
                }
                catch
                {
                    i--;
                }
                if (i == 3)
                    break;
            }
        }
    }
}
