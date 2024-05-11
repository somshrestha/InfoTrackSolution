namespace InfoTrack.Api.Dtos
{
    public class SearchResultDto
    {
        public string Url { get; set; }
        public string SearchTerm { get; set; }
        public string Ranking { get; set; }
        public DateTime SearchDate { get; set; }
    }
}
