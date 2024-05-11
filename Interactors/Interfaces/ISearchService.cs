using InfoTrack.Api.Dtos;

namespace InfoTrack.Data.Interfaces
{
    public interface ISearchService
    {
        Task<SearchResultDto> Search(SearchDto dto);
    }
}
