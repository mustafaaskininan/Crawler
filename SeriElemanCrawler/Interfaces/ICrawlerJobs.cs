using System.Collections.Generic;
using SeriElemanCrawler.Models;
using SeriElemanCrawler.Models.Entities;

namespace SeriElemanCrawler.Interfaces
{
    public interface ICrawlerJobs
    {
        void Add(List<CrawlerModel> crawlerModels);

        List<CrawlerJobs> Get();
    }
}