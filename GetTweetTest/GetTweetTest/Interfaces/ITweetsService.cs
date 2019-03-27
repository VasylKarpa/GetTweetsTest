using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GetTweetTest.Models;

namespace GetTweetTest.Interfaces {
  public interface ITweetsService {
    Task<List<Tweet>> GetTweets(string url, DateTime startDate, DateTime endDate);
  }
}
