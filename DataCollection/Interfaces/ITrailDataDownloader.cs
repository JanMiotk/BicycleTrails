using HtmlAgilityPack;
using Models;

namespace Parser.Interfaces
{
    public interface ITrailDataDownloader : IDataDownloader
    {
        Trail AddToList(HtmlNode line);
        byte[] DownloadPhoto(string url);
        string ReturnAuthor(HtmlNode line);
        string ReturnDataFromInfoNode(HtmlNode line, int index);
        string ReturnDificulty(HtmlNode line);
        string ReturnDistance(HtmlNode line);
        string ReturnDuration(HtmlNode line);
        string ReturnLink(HtmlNode line);
        string ReturnLocation(HtmlNode line);
        HtmlDocument ReturnPage(string url);
        HtmlNode ReturnPartOfRow(HtmlNode line, int index);
        byte[] ReturnPicture(HtmlNode line, string url, int index);
        int ReturnQuantityOfPages(HtmlDocument htmlbody);
        double ReturnRating(HtmlNode line);
        string ReturnTitle(HtmlNode line);
    }
}