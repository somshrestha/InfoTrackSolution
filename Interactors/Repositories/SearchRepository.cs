using InfoTrack.Data.Data;
using InfoTrack.Data.Models;
using InfoTrack.Interactors.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InfoTrack.Interactors.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly AppDbContext _context;

        public SearchRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SearchResult searchResult)
        {
            await _context.SearchResults.AddAsync(searchResult);

            await _context.SaveChangesAsync();
        }

        public async Task<IList<SearchResult>> GetAllSearchHistory()
        {
            return await _context.SearchResults
                            .OrderByDescending(sr => sr.SearchDate)
                            .ToListAsync();
        }
    }
}
