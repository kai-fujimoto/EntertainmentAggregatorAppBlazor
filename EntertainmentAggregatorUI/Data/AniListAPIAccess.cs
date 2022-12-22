
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using static EntertainmentAggregatorUI.Data.AniListContainer;

namespace EntertainmentAggregatorUI.Data
{
    public class AniListAPIAccess
    {
        public static GraphQLHttpClient client = new GraphQLHttpClient("https://graphql.anilist.co/", new NewtonsoftJsonSerializer());

        public static async Task<List<Medium>> getCurrentSeasonalAnime()
        {
            var now = DateTime.Now;
            string season;
            if (now.Month > 0 && now.Month < 4)
            {
                season = "WINTER";
            }
            else if (now.Month > 3 && now.Month < 7)
            {
                season = "SPRING";
            }
            else if (now.Month > 7 && now.Month < 10)
            {
                season = "SUMMER";
            }
            else
            {
                season = "FALL";
            }

            //Console.WriteLine($"{now.Year}, {now.Year.GetType()}");
            Console.WriteLine(season);

            var seasonalAnimePage1 = await doQLQuery(1, season, now.Year);
            var seasonalAnimePage2 = await doQLQuery(2, season, now.Year);

            var seasonalAnime = seasonalAnimePage1.Page.media;
            seasonalAnime.AddRange(seasonalAnimePage2.Page.media);
            return seasonalAnime;

        }

        //Add a different query to go by airing status; if it is within two seasons of the current show it there;
        // will keep out long running series like one piece that don't need to be shown


        public static async Task<Root> doQLQuery(int pageNum, string seasonofYear, int seasonYearNum)
        {
            var animeMediaRequest = new GraphQLRequest
            {
                Query = @"
                query ($page: Int, $season: MediaSeason, $seasonYear: Int) { 
                    Page(page:$page, perPage:50) {
                        pageInfo{
                          total
                          currentPage
                          lastPage
                          hasNextPage
                          perPage
                        }
                        media(season:$season, seasonYear:$seasonYear, type:ANIME, sort:SCORE_DESC){
                          id
                          meanScore
                          isAdult
                          averageScore
                          episodes
                          duration
                          description(asHtml: false)
                          title{
                            english
                            romaji
                          }
                          format
                          studios(isMain: true) {
                            nodes{
                                name
                            }
                          }
                          genres
                          source
                          externalLinks {
                            site
                            url
                            icon
                            color
                          }
                          coverImage {
                            extraLarge
                            large
                            medium
                            color
                          }
                          siteUrl
                          nextAiringEpisode {
                            airingAt
                            episode
                          }
      
                        }
                      }
            }",
                Variables = new
                {
                    page = pageNum,
                    season = seasonofYear,
                    seasonYear = seasonYearNum

                }
            };

            //Skip root class -> goes directly into data
            var graphQLResponse = await client.SendQueryAsync<Root>(animeMediaRequest);
            var seasonalAnime = graphQLResponse.Data;
            return seasonalAnime;


        }
        
        // Filter to remove adult, movies, and OVAs; keep normal TV and ONA anime
        public static List<Medium> filterForMainSesaonal(List<Medium> toFilter)
        {
            List<Medium> filtered = new List<Medium>();
            foreach (var anime in toFilter)
            {
                //Guard clause
                if (anime.isAdult || anime.format == "MOVIE" || anime.format == "SPECIAL" || anime.format == "OVA" || anime.format == "MUSIC" || anime.meanScore == null)
                {
                    continue;
                }

                filtered.Add(anime);


            }
            return filtered;
        }

        public static int animeComparison(Medium one, Medium two)
        {
            if (one.meanScore < two.meanScore)
            {
                return -1;
            }
            else if (one.meanScore == two.meanScore)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static List<ExternalLink> linkFilter(Medium anime)
        {
            var filtered = new List<ExternalLink>();
            foreach (var link in anime.externalLinks)
            {
               if (link.site != "Twitter" && link.site != "YouTube" && link.site != "Official Site" && link.site != "Instagram"
                            && link.site != "Bilibili TV" && link.site != "TikTok")
                {
                    filtered.Add(link);
                }
            }
            return filtered;
        }

        public static string calcTimeUntil(Medium anime)
        {
            var now = DateTime.Now;
            var nextEp = DateTimeOffset.FromUnixTimeSeconds(anime.nextAiringEpisode.airingAt);
            var timeBetween = nextEp - now;
            return $@"EP{anime.nextAiringEpisode.episode}: {timeBetween.Days} Days {timeBetween.Hours} Hours";


        }

        public static Dictionary<string, List<Medium>> groupByDay(List<Medium> anime)
        {
            Dictionary<string, List<Medium>> grouped = new Dictionary<string, List<Medium>>();

            grouped["Sunday"] = new List<Medium>();
            grouped["Monday"] = new List<Medium>();
            grouped["Tuesday"] = new List<Medium>();
            grouped["Wednesday"] = new List<Medium>();
            grouped["Thursday"] = new List<Medium>();
            grouped["Friday"] = new List<Medium>();
            grouped["Saturday"] = new List<Medium>();

            foreach(var show in anime)
            {
                if (show.nextAiringEpisode == null)
                {
                    continue;
                }
                grouped[dateToDay(show.nextAiringEpisode.airingAt)].Add(show);
            }
            return grouped;
        }
        

        public static string dateToDay(long timeStamp)
        {
            var datTimeOffSet = DateTimeOffset.FromUnixTimeSeconds(timeStamp);
            DateTime date = datTimeOffSet.DateTime;
            date = date.AddHours(-8);
            return date.DayOfWeek.ToString();
        }
    }
}

