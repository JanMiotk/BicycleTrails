using HtmlAgilityPack;

namespace ParserTests.Interfaces
{
    public interface IAditionalInformationsModel
    {
        HtmlDocument ReturnExceeding();
        public HtmlDocument ReturnKindOfActivity();
    }
}