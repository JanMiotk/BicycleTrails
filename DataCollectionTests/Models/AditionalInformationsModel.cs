using HtmlAgilityPack;
using ParserTests.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParserTests.Models
{
    public class AditionalInformationsModel : IAditionalInformationsModel
    {
        public HtmlDocument ReturnKindOfActivity()
        {
            HtmlDocument document = new HtmlDocument();
            string Text = "<div><span class='kind-of-act'>rower górski</span></div>";
            document.LoadHtml(Text);
            return document;
        }
        public HtmlDocument ReturnExceeding()
        {
            HtmlDocument document = new HtmlDocument();
            string Text = "<div><span class='exceeding '>150 m</span><div><span class='exceeding '>350 m</span></div></div>";
            document.LoadHtml(Text);
            return document;
        }
    }
}
