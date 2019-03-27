import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class TweetsService {
  private url = '/api/tweets/timeInterval?startdate=2016-01-01&enddate=2018-01-01';
  constructor(private http: HttpClient) { }
  getTweets() {
    return this.http.get(this.url);
  }
}
