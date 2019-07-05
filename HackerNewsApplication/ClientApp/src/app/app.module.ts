import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { DatePipe } from '@angular/common';
import { PaginatorModule, Paginator } from 'primeng/paginator';
import { ButtonModule } from 'primeng/button';
import { DropdownModule, PickList, SharedModule, PanelModule } from 'primeng/primeng';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { DialogModule, Dialog } from 'primeng/dialog'
import { DataViewModule } from 'primeng/dataview';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { ArticleService } from './article.service';
import { ArticleComponent } from './article/article.component';
import { AppHeaderComponent } from './app-header/app-header.component';
import { LoadingScreenComponent } from './loading-screen/loading-screen.component';

@NgModule({
  declarations: [
    AppComponent,
    ArticleComponent,
    AppHeaderComponent,
    LoadingScreenComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    DialogModule,
    PanelModule,
    SharedModule,
    DataViewModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FormsModule,
    ButtonModule,
    PaginatorModule,
    DropdownModule,
    RouterModule.forRoot([
      { path: '', component: ArticleComponent }
    ])
  ],
  providers: [ArticleService, DatePipe],
  bootstrap: [AppComponent]
})

export class AppModule { }
