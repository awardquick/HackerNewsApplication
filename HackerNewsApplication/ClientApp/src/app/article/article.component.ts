import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { DropdownModule, LazyLoadEvent, SharedModule, PanelModule } from 'primeng/primeng';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PaginatorModule } from 'primeng/paginator';
import { DialogModule, Dialog } from 'primeng/dialog'
import { Article } from 'src/app/model/article';
import { ArticleService } from '../article.service';
import { SelectItem } from 'primeng/primeng';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css']
})
export class ArticleComponent implements OnInit {

  public articles: Article[] = [];

  SelectItem: any;

  dataSource: Article[];

  totalRecords: number;

  sortOptions: SelectItem[];

  selectedArticle: Article;

  displayDialog: boolean;

  loading: boolean;

  constructor(private articleSvc: ArticleService, private datePipe: DatePipe) { }

  ngOnInit() {
    this.retrieveArticles();
    this.sortOptions = [
      { label: 'Oldest First', value: 'time' }
    ];
  }
  selectArticle(event: Event, article: Article) {
    this.selectedArticle = article;
    this.displayDialog = true;
    event.preventDefault();
  }

  async retrieveArticles() {
    let articleNumber = 600;
    await this.articleSvc.getArticles(articleNumber).subscribe(success => {
      if (success) {
        this.articles = this.articleSvc.articles;
        this.dataSource = this.articles;
        this.totalRecords = this.dataSource.length;
      }
    })
  }

  loadArticlesLazy(event: LazyLoadEvent) {
    setTimeout(() => {
      if (this.dataSource) {
        this.articles = this.dataSource.slice(event.first, (event.first + event.rows));
        this.loading = false;
      }
    }, 1000);
  }



 


  onDialogHide() {
    this.selectedArticle = null;
  }



}
