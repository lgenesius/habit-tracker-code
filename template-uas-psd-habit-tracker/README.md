# Habit Tracker : HTTP Endpoint

HTTP Endpoint can be open in the folder [Abc.HabitTracker.Api/Controllers](Abc.HabitTracker.Api/Controllers)

## Run Guide
1. In the terminal, type : cd Abc.HabitTracker.Api<br>
2. Then type : dotnet run, and see what localhost it is.<br>
3. Then in the Postman, make sure it is the right type (for example we want to get the GET endpoint, so make sure that it is GET in the Postman), then type http://localhost:5000/ if the localhost is 5000 in http<br>
3. Then copy the endpoint like api/v1/users/{userID}/habits from HabitsController folder and paste to the Postman after http://localhost:5000/ , make sure it is the right endpoint<br>
4. Change the {userID} with GUID Id, you can find the examples in the PostgreSQL Database (you can use GUID Id like 2b13c1fe-a374-4df8-ba9a-0aa7744a4531).
