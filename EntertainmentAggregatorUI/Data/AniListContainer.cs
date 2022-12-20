using Newtonsoft.Json;

namespace EntertainmentAggregatorUI.Data
{
    public class AniListContainer
    {
        public class CoverImage
        {
            [JsonProperty("extraLarge")]
            public string extraLarge { get; set; }

            [JsonProperty("large")]
            public string large { get; set; }

            [JsonProperty("medium")]
            public string medium { get; set; }

            [JsonProperty("color")]
            public string color { get; set; }
        }

        public class Root
        {
            [JsonProperty("Page")]
            public Page Page { get; set; }
        }

        public class ExternalLink
        {
            [JsonProperty("site")]
            public string site { get; set; }

            [JsonProperty("url")]
            public string url { get; set; }

            [JsonProperty("icon")]
            public string icon { get; set; }

            [JsonProperty("color")]
            public string color { get; set; }
        }

        public class Medium
        {
            [JsonProperty("id")]
            public int id { get; set; }

            [JsonProperty("meanScore")]
            public Nullable<int> meanScore { get; set; }

            [JsonProperty("averageScore")]
            public Nullable<int> averageScore { get; set; }

            [JsonProperty("isAdult")]
            public bool isAdult { get; set; }

            [JsonProperty("episodes")]
            public int? episodes { get; set; }

            [JsonProperty("duration")]
            public int? duration { get; set; }

            [JsonProperty("description")]
            public string description { get; set; }

            [JsonProperty("title")]
            public Title title { get; set; }

            [JsonProperty("format")]
            public string format { get; set; }

            [JsonProperty("studios")]
            public Studios studios { get; set; }

            [JsonProperty("genres")]
            public List<string> genres { get; set; }

            [JsonProperty("source")]
            public string source { get; set; }

            [JsonProperty("externalLinks")]
            public List<ExternalLink> externalLinks { get; set; }

            [JsonProperty("coverImage")]
            public CoverImage coverImage { get; set; }

            [JsonProperty("siteUrl")]
            public string siteUrl { get; set; }

            [JsonProperty("nextAiringEpisode")]
            public NextAiringEpisode nextAiringEpisode { get; set; }
        }

        public class NextAiringEpisode
        {
            [JsonProperty("airingAt")]
            public int airingAt { get; set; }

            [JsonProperty("episode")]
            public int episode { get; set; }
        }

        public class Node
        {
            [JsonProperty("name")]
            public string name { get; set; }
        }

        public class Page
        {
            [JsonProperty("pageInfo")]
            public PageInfo pageInfo { get; set; }

            [JsonProperty("media")]
            public List<Medium> media { get; set; }
        }

        public class PageInfo
        {
            [JsonProperty("total")]
            public int total { get; set; }

            [JsonProperty("currentPage")]
            public int currentPage { get; set; }

            [JsonProperty("lastPage")]
            public int lastPage { get; set; }

            [JsonProperty("hasNextPage")]
            public bool hasNextPage { get; set; }

            [JsonProperty("perPage")]
            public int perPage { get; set; }
        }
        /*
        public class Root
        {
            [JsonProperty("data")]
            public Data data { get; set; }
        }
        */
        public class Studios
        {
            [JsonProperty("nodes")]
            public List<Node> nodes { get; set; }
        }

        public class Title
        {
            [JsonProperty("english")]
            public string english { get; set; }

            [JsonProperty("romaji")]
            public string romaji { get; set; }
        }
    }
}
