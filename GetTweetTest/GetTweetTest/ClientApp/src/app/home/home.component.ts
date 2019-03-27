import { Component, OnInit } from '@angular/core';
import { TweetsService } from '../services/tweets.service';
import { Tweet } from '../models/tweet';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  providers: [TweetsService]
})
export class HomeComponent implements OnInit{
  tweets: Array<Tweet>;
  showText: boolean = true;

  constructor(private tweetsService: TweetsService) {
    this.tweets = new Array<Tweet>();
  }

  ngOnInit() {
    this.loadTweets();
    
  }

  private loadTweets() {
    this.tweetsService.getTweets().subscribe((data: Tweet[]) => {
      this.tweets = data;
      this.showText = false;
    });
    
  }
}
