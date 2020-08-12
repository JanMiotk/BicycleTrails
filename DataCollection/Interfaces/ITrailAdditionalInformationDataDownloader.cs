using HtmlAgilityPack;
using Parser.Interfaces;

namespace DownloadDataFromTraseo.Interfaces
{
    public interface ITrailAdditionalInformationDataDownloader : IDataDownloader
    {
        string ReturnAverageSpeed(HtmlDocument document);
        string ReturnData(HtmlDocument document);
        string ReturnDescription(HtmlDocument document);
        string ReturnDifficulty(HtmlDocument document);
        string ReturnDistance(HtmlDocument document);
        string ReturnDuration(HtmlDocument document);
        string ReturnExceeding(HtmlDocument document);
        string ReturnKindOfActivity(HtmlDocument document);
        string ReturnLocation(HtmlDocument document);
        HtmlDocument ReturnPage(string url);
        string ReturnRating(HtmlDocument document);
        string ReturnSumDown(HtmlDocument document);
        string ReturnSumUp(HtmlDocument document);
    }
}