namespace Yorgi.FilmWebApi.Models
{
    public sealed class SearchParameters
    {
        public string Title { get; }
        public int? Year { get; }
        public bool IdOnly { get; }
        public QueryType QueryType { get; }

        public SearchParameters(string title, QueryType queryType, bool idOnly, int? year = null)
        {
            this.Title = title;
            this.Year = year;
            this.IdOnly = idOnly;
            this.QueryType = queryType;
        }
    }

    public enum QueryType
    {
        Film,
        Serial
    }
}