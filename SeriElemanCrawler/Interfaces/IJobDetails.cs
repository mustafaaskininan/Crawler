using System.Collections.Generic;
using SeriElemanCrawler.Models;

namespace SeriElemanCrawler.Interfaces
{
    public interface IJobDetails
    {
        List<CrawlerModel> GetJobsDetail(List<string> myLinks);
    }
}