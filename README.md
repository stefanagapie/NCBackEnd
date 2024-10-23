# NCBackEnd
SE Trial Project

## An ASP.NET Web API Project
This project exposes an api that computes the following statistics on data pulled from https://randomuser.me/

## The following stats are computed
- Percentage of gender in each category
- Percentage of first names that start with A-M versus N-Z
- Percentage of last names that start with A-M versus N-Z
- Percentage of people in each state, up to the top 10 most populous states
- Percentage of females in each state, up to the top 10 most populous states
- Percentage of males in each state, up to the top 10 most populous states
- Percentage of people in the following age ranges: 0-20, 21-40, 41-60, 61-80, 81-100, 100+

## The API can return the stats in the following format
- text/json, text/xml and text/plain
The client my specify the type by providing accept headers value when making the request
