using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SeriElemanCrawler.Interfaces;
using SeriElemanCrawler.Models.Entities;

namespace SeriElemanCrawler.Controllers
{
    [Route("api/crawler")]
    [ApiController]
    public class CrawlerController : ControllerBase
    {
        private readonly ICrawlerJobs _crawlerJobs;
        private readonly IJobDetails _jobDetails;
        private readonly IPageLinks _pageLinks;

        public CrawlerController(IPageLinks pageLinks, IJobDetails jobDetails, ICrawlerJobs crawlerJobs)
        {
            _pageLinks = pageLinks;
            _jobDetails = jobDetails;
            _crawlerJobs = crawlerJobs;
        }

        [HttpGet("{pageNumber}")]
        public IActionResult Get(int pageNumber)
        {
            var pageLinksList = _pageLinks.GetPageLinksList(pageNumber);
            var jobDetailList = _jobDetails.GetJobsDetail(pageLinksList);
            _crawlerJobs.Add(jobDetailList);

            return NoContent();
        }

        [HttpGet]
        public List<CrawlerJobs> Get()
        {
            var crawlerJobs = _crawlerJobs.Get();

            return crawlerJobs;
        }
    }
}