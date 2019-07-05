import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { map } from "rxjs/operators";
import { Observable } from 'rxjs';
import { Article } from 'src/app/model/article';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {
  public articles: Article[] = [];
  constructor(private httpClient: HttpClient) { }

  getArticles(articleNumber: number) {
    return this.httpClient.get('/api/Article/GetArticles/' + articleNumber)
      .pipe(
        map((data: any[]) => {
          this.articles = data;
          return true;
        }));
  }
}
