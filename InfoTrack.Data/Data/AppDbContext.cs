using InfoTrack.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InfoTrack.Data.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<SearchResult> SearchResults { get; set; }
    }
}
