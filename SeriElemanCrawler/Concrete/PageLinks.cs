using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using SeriElemanCrawler.Interfaces;

namespace SeriElemanCrawler.Concrete
{
    public class PageLinks : IPageLinks
    {
        public List<string> GetPageLinksList(int pageNumber)
        {
            var myLinks = new List<string>();

            for (var i = 0; i < pageNumber; i++)
            {
                var seriElemanUrl = i == 0
                    ? "https://serieleman.com/is_ilanlari/?ilan=&sehir=T%C3%BCm%20%C5%9Eehirler&gun=&cinsiyet=&alan=&per_page="
                    : $"https://serieleman.com/is_ilanlari/?ilan=&sehir=T%C3%BCm%20%C5%9Eehirler&gun=&cinsiyet=&alan=&per_page={i + 1}";

                var url = new Uri(
                    seriElemanUrl);
                var client = new WebClient();
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var html = client.DownloadString(url);
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                var pageLinks = htmlDoc.DocumentNode.SelectNodes("//div[@class='search-jobs-list-div']/table/tr/td/b")
                    .Select(node => node.SelectSingleNode(".//a").Attributes["href"].Value).ToList();

                myLinks.AddRange(pageLinks);
            }

            return myLinks;
        }
    }
}