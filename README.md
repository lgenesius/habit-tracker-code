# Habit Tracker Backend Code
This is a Habit Tracker Backend Code implemented Domain Driven Design and HTTP Api.

## Description
There is also Badge that act like a reward if the users meet the requirements to obtain the badge (Badge can't be duplicated).<br>
The requirements are :<br>
▸ Obtain Dominating Badge if the user get 4 streaks without do a habit in the wrong day<br>
▸ Obtain Workaholic Badge if the user do the habit for 10 times. Doing the habit on the wrong day is also counted.<br>
▸ Obtain Epic Comeback Badge if the user do the habit in the wrong day for 10 streaks, and then do the habit on the right day for 10 streaks.<br><br>

The Domain Driven Design implemented in this code are DDD Building Block :<br>
▸ Value Object<br>
▸ Entity<br>
▸ Aggregate<br>
▸ Domain Service<br>
▸ Factory<br>
▸ Repository<br><br>

There is 7 HTTP Endpoint :<br>
▸ Get(GET) all habit endpoint.<br>
▸ Get(GET) a habit with the right user id and habit id endpoint.<br>
▸ Add(POST) new habit according to the user id endpoint with requested name(required) and days(optional) from Postman and return the value endpoint.<br>
▸ Update(PUT) the habit's name with the right user id, habit id and with requested name from Postman and return the value endpoint.<br>
▸ Delete(DELETE) a habit with the right user id and habit id and return the value endpoint.<br>
▸ Do habit(POST) with the right user id and habit id and return the value endpoint.<br>
▸ Get all the badge(GET) with the right user id.

## Project's Image Overview
<p align="center"><img src="habit tracker pict postman.png"></p><br>

## Technologies
▸ C#
▸ Asp.Net
▸ PostgreSQL
▸ Postman
▸ Visual Studio Code


