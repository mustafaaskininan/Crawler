using System;

namespace SeriElemanCrawler.Models
{
    public class CrawlerModel
    {
        public int Id { get; set; }
        public string JobPostingName { get; set; }
        public string CompanyName { get; set; }
        public DateTime JobDate { get; set; }
        public string City { get; set; }
        public string GenderAge { get; set; }
        public string WorkDetail { get; set; }

        public string JobDetail { get; set; }
        public string PhoneUrl { get; set; }
        public string MobilePhoneUrl { get; set; }
        public string FaxUrl { get; set; }
        public string ContactPersonName { get; set; }
        public string Sector { get; set; }
    }
}