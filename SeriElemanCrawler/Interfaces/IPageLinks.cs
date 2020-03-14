using System.Collections.Generic;

namespace SeriElemanCrawler.Interfaces
{
    public interface IPageLinks
    {
        List<string> GetPageLinksList(int pageNumber);
    }
}