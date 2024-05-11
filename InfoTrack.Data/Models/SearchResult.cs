using InfoTrack.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoTrack.Data.Models
{
    public class SearchResult : BaseModel
    {
        public string Url { get; set; } = string.Empty;
        public string SearchTerm { get; set; } = string.Empty;
        public string Ranking { get; set; } = string.Empty;
        public DateTime SearchDate { get; set; }

        public SearchResultDto Convert()
        {
            return new SearchResultDto
            {
                Url = this.Url,
                SearchTerm = this.SearchTerm,
                Ranking = this.Ranking,
                SearchDate = this.SearchDate
            };
        }
    }
}
