using InfoTrack.Data.Models;

namespace InfoTrack.Interactors.Interfaces
{
    public interface ISearchRepository
    {
        Task AddAsync(SearchResult searchResult);
        Task<IList<SearchResult>> GetAllSearchHistory();
    }
}
