Eppendorf_CRUD

This web application is a basic CRUD implementation based on .NET 6.

It is used to process CRUD operations on given Device data.

Structure:
	-	Data: containing database, entities end repositories
	-	Eppendorf_CRUD: Webapplication Frontend
Database:
	- local SQLite database named EDB located at Data/DB
	- EntityFrameWorkCore 6 (Code First)

On Startpage it is possible to upload the initial data.json file, which will be stored in the database. (Data is already existing)
Further more there are the CRUD operation pages for the devices located in folder "Pages/DevidePages".

The data.json file can be found in Data/TestDataFile

There is no general application stylingand further validation and error handling yet, because of time reason.

For a clean startup:
	- delete the database
	- run in Data project console "Update-Database" (Migrations are existing)