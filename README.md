# UserManagement

To run the project:

1. Have a running PostgreSQL database;
2. Inside the database create a new schema named "UserManagement";
3. Changed connection string in both UserMangement.API and UserManagement.API.IntegrationTests projects;
4. Run tests to make sure that database connection works.


Kodas atlieka užduotis aprašytas čia:
Privaloma dalis.

Sukurti naudotojų valdymo (user management) sistemos POC. Sistema realizuojama REST API. Privalomi endpoint‘ai realizuojantys CRUD  operacijas:

·   parodyti naudotojų sąrašą,

·   sukurti naują naudotoją,

·   sukurti naujus naudotojus iš sąrašo faile,

·   redaguoti esamą,

·   ištrinti naudotoją.

Duomenys saugomi duomenų bazėje.

Technologijos:

Backend: .Net 5 - Asp.Net WebApi
DB:  Postgresql
Užduoties sprendimą patalpinti, kurioje nors git based sistemoje GitHub, GitLab ar panašioje ir pateikti prisijungimą patikrinimui.
