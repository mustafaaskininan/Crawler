using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using SeriElemanCrawler.Interfaces;
using SeriElemanCrawler.Models;

namespace SeriElemanCrawler.Concrete
{
    public class JobDetails : IJobDetails
    {
        public List<CrawlerModel> GetJobsDetail(List<string> myLinks)
        {
            var myDataList = new List<CrawlerModel>();

            foreach (var myLink in myLinks)
            {
                var url = new Uri(
                    myLink);
                var client = new WebClient();
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var html = client.DownloadString(url);

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                var myData = new CrawlerModel();
                var jobDetail = new List<string>();
                var contactImgUrlDictionary = new Dictionary<string, string>();
                var contactKeyword = new Dictionary<string, string>
                {
                    {"Phone", "tur=1"},
                    {"MobilePhone", "tur=2"},
                    {"Fax", "tur=3"}
                };

                foreach (var node in htmlDoc.DocumentNode.SelectNodes("//div[@class='ad-info']"))
                {
                    myData.JobPostingName = Regex.Replace(htmlDoc.DocumentNode.Descendants("h1").First().InnerHtml,
                        @"\t|\n|\r", "");
                    myData.CompanyName = Regex.Replace(htmlDoc.DocumentNode.Descendants("b").First().InnerText,
                        @"\t|\n|\r", "");

                    jobDetail.AddRange(node.SelectNodes("//div[@class='jobs-details-table']/table//tr//td")
                        .Select(nodeDetail => Regex.Replace(nodeDetail.InnerText, @"\t|\n|\r", ""))
                        .Where(value => !string.IsNullOrEmpty(value)));

                    foreach (var nodeDetail in node.SelectNodes("//div[@class='jobs-details-table']//img"))
                    {
                        var contactImgUrl = nodeDetail.GetAttributeValue("src", "");

                        if (string.IsNullOrEmpty(contactImgUrl)) continue;

                        foreach (var (key, value) in contactKeyword)
                        {
                            var contains = contactImgUrl.Contains(value);
                            if (contains) contactImgUrlDictionary.Add(key, contactImgUrl);
                        }
                    }
                }

                myData.JobDate = Convert.ToDateTime(jobDetail[0]);
                myData.City = jobDetail[1];
                myData.GenderAge = jobDetail[2];
                myData.WorkDetail = jobDetail[3];
                myData.JobDetail = jobDetail[4];
                myData.ContactPersonName = jobDetail[5];
                myData.Sector = jobDetail[6];
                myData.FaxUrl = contactImgUrlDictionary.TryGetValue("Fax", out var tmp) ? tmp : "";
                myData.PhoneUrl = contactImgUrlDictionary.TryGetValue("Phone", out var tmp1) ? tmp1 : "";
                myData.MobilePhoneUrl = contactImgUrlDictionary.TryGetValue("MobilePhone", out var tmp2) ? tmp2 : "";

                myDataList.Add(myData);
            }

            return myDataList;
        }
    }
}