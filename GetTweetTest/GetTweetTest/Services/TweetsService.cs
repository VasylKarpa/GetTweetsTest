using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GetTweetTest.Interfaces;
using GetTweetTest.Models;
using Newtonsoft.Json;

namespace GetTweetTest.Services {
  public class TweetsService : ITweetsService {
    
    public async Task<List<Tweet>> GetTweets(string url, DateTime startDate, DateTime endDate) {

      var client = new HttpClient();
      var tweets = new List<Tweet>();

      await GetTweetsFromApi(url, tweets, client, startDate, endDate);
      return tweets
               .OrderBy(t => t.Stamp)          
               .GroupBy(t => t.Id)            
               .Select(grp => grp.First())    
               .ToList();

    }

    private async Task GetTweetsFromApi(string url, List<Tweet> tweets, HttpClient client, DateTime startDate, DateTime endDate) {
      var requestUrl = $"{url}?startDate={startDate:yyyy-MM-ddThh:mm:ssZ}&endDate={endDate:yyyy-MM-ddThh:mm:ssZ}";
      var response = await client.GetStringAsync(requestUrl);
      var tweetsFromBadApi = JsonConvert.DeserializeObject<List<Tweet>>(response);

      if (tweetsFromBadApi.Count == 100) {
        var firstHalfStartDate = startDate;
        var firstHalfEndDate = GetHalfDate(startDate, endDate);

        await GetTweetsFromApi(url, tweets, client, firstHalfStartDate, firstHalfEndDate);

        var secondHalfStartDate = firstHalfEndDate;
        var secondHalfEndDate = endDate;

        await GetTweetsFromApi(url, tweets, client, secondHalfStartDate, secondHalfEndDate);
      }
      else    // if records a less then 100 we add them to final list
        tweets.AddRange(tweetsFromBadApi);

    }
    private static DateTime GetHalfDate(DateTime start, DateTime end) {
      var ticks = (end.Ticks - start.Ticks) / 2;

      return new DateTime(start.Ticks + (ticks > 0 ? ticks : 1));
    }
  }
}
