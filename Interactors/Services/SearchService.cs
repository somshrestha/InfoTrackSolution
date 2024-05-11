using InfoTrack.Api.Dtos;
using InfoTrack.Data.Interfaces;
using InfoTrack.Data.Models;
using InfoTrack.Interactors.Interfaces;
using InfoTract.Helpers;
using System.Text.RegularExpressions;

namespace InfoTrack.Interactors.Services
{
    public class SearchService : ISearchService
    {
        private readonly HttpClient _httpClient;
        private readonly ISearchRepository _searchRepository;

        public SearchService(HttpClient httpClient, ISearchRepository searchRepository)
        {
            _httpClient = httpClient;
            _searchRepository = searchRepository;
        }

        public async Task<SearchResultDto> Search(SearchDto dto)
        {
            var searchUrl = $"http://www.google.co.uk/search?num={Constants.DefaultNumberOfSearchResults}&q={dto.SearchTerm}";

            var response = await _httpClient.GetStringAsync(searchUrl);

            var results = GetResults(response, dto.Url);

            var searchResult = new SearchResult
            {
                Url = dto.Url,
                SearchTerm = dto.SearchTerm,
                Ranking = string.Join(", ", results),
                SearchDate = DateTime.UtcNow
            };

            await _searchRepository.AddAsync(searchResult);

            var dtoEntity = searchResult.Convert();

            return dtoEntity;
        }

        private List<string> GetResults(string response, string url)
        {
            var regex = "href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))";

            var linksFound = Regex.Matches(response, regex, RegexOptions.IgnoreCase);

            var results = new List<string>();
            int position = 0;

            foreach (Match item in linksFound)
            {
                if (item.Value.Contains(url))
                {
                    results.Add(position.ToString());
                }
                position++;
            }

            return results;
        }
    }
}
