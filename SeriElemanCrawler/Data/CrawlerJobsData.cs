using System.Collections.Generic;
using System.Linq;
using SeriElemanCrawler.DbContext;
using SeriElemanCrawler.Interfaces;
using SeriElemanCrawler.Models;
using SeriElemanCrawler.Models.Entities;

namespace SeriElemanCrawler.Data
{
    public class CrawlerJobsData : ICrawlerJobs
    {
        public void Add(List<CrawlerModel> crawlerModels)
        {
            using var context = new SeriElemanContext();
            context.Database.EnsureCreated();
            foreach (var crawlerModel in crawlerModels)
            {
                context.CrawlerJobs.Add(new CrawlerJobs
                {
                    CompanyName = crawlerModel.CompanyName,
                    JobPostingName = crawlerModel.JobPostingName,
                    City = crawlerModel.City,
                    ContactPersonName = crawlerModel.ContactPersonName,
                    FaxUrl = crawlerModel.FaxUrl,
                    GenderAge = crawlerModel.GenderAge,
                    JobDate = crawlerModel.JobDate,
                    JobDetail = crawlerModel.JobDetail,
                    MobilePhoneUrl = crawlerModel.MobilePhoneUrl,
                    PhoneUrl = crawlerModel.PhoneUrl,
                    Sector = crawlerModel.Sector,
                    WorkDetail = crawlerModel.WorkDetail
                });

                context.SaveChanges();
            }
        }

        public List<CrawlerJobs> Get()
        {
            using var context = new SeriElemanContext();
            var myData = context.CrawlerJobs.ToList();

            return myData;
        }
    }
}