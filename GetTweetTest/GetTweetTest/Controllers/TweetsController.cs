using System;
using System.Threading.Tasks;
using GetTweetTest.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GetTweetTest.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class TweetsController : ControllerBase {
    private readonly IConfiguration _configuration;
    private readonly ITweetsService _tweetsService;

    public TweetsController(IConfiguration configuration, ITweetsService tweetsService) {
      _configuration = configuration;
      _tweetsService = tweetsService;
    }

    [HttpGet]
    [Route("timeInterval")]
    public async Task<IActionResult> GetTweets([FromQuery] DateTime startDate, DateTime endDate) {
      if (startDate == default(DateTime) || endDate == default(DateTime)) {
        return BadRequest("Please specify date, for example /api/tweets/timeInterval?startdate=2016-01-01&enddate=2018-01-01");
      }

      var badApiUrl = _configuration["ApiUrl"];
      var tweets = await _tweetsService.GetTweets(badApiUrl, startDate, endDate);

      return Ok(tweets);
    }
  }
}
