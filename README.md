# HackerNewsApplication

## Challenge Instructions 

Create an application that retrieved the top articles from Hacker News.
Bonus Should be written in C# and Angular.
Should be tested to ensure functionality. 
Bonus if it is deployed to azure 
Bonus add search function
Bonus add caching

## Design Overview
This solution was built from the Visual Studio ASP.NET Core 2.2 Angular template. The design is based off the CLEAN architechture, which focuses 
on removing dependencies and more modularity in the code base. 

## Client-Side
The frontend of this application is Angular 7 which is rendered from the appModule.html The commponents are angular with primeNG. 

## ServerSide

##### Core
The core of this application holds the base entity, interfaces, and services

##### Infrastructue
This section has the mappers which will be used to to map the base entity to the model used on the front end and in the api. This level also holds 
the APIClient and the hacker news service. 

##### HackerNewsWebApp
Other then the startup where all the necessary portions are tied together, the main controller logic to retrieve the articles is also held here. 

