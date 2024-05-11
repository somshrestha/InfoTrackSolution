using InfoTrack.Data.Data;
using InfoTrack.Data.Models;
using InfoTrack.Interactors.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
