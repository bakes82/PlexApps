namespace PlexApps.Dto
{
    public class ShowDto
    {
        public string Title { get; set; }
        public int? Year { get; set; }
        public bool PlexHas { get; set; }
        public bool TraktHas { get; set; }
        public bool PlexWatched { get; set; }
        public bool TraktWatched { get; set; }
        public string Description { get; set; }
        public string Art { get; set; }
        public string IMDB { get; set; }
        public string Duration { get; set; }
        public string Rating { get; set; }
        public string PlexRatingKey { get; set; }
    }
}