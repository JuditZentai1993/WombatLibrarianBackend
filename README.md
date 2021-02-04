# The Wombat Librarian

This is the backend (built with ASP.NET Core) part of our web-application made for book lovers, where people can browse books, keep track of what books they have and what books they want to read. The aim of the site to create a community where the users can trade, rate, and discuss books.
To give you an idea, the aim is to make it similar to https://www.goodreads.com/, or the hungarian moly.hu (https://hu.wikipedia.org/wiki/Moly.hu)

Frontend repository can be found here: https://github.com/Dusernajder/Wombat_librarian

## Project Status

This project is currently in development. The user can add books to his/her bookshelf and wishlist, which is stored on the server side in a database with the help of Entity Framework. The current sprint's scope is to implement registration/login on the website, trach individual bookshelves and wishlists per user.

Further tasks for subsequent sprints: develop the community side of the webpage, make following people possible, add rating and comment section to book, create a forum as part of the web application.

## Project Screenshots

Front page:
![Welcome page](https://i.ibb.co/2czn1dC/wombat-Librarian01.png)

Results of a search for "Asimov":

![Search results](https://i.ibb.co/XJDFRtL/wombat-Librarian02.png)

Details of a selected book:

![Book details](https://i.ibb.co/dD2vFRn/wombat03.png)

The current state of our bookshelf (after adding some books to it):

![Bookshelf content with books](https://i.ibb.co/mtdmDnx/wombat04.png)

How our wishlist looks without adding any books to it:

![Wishlist content without books](https://i.ibb.co/vVLL3ZY/wombat05.png)

## Installation and Setup Instructions

1. Clone this repository. You also have to clone the frontend repository and follow the instruction in the readme file you can find there.
   Frontend repository can be found here: https://github.com/Dusernajder/Wombat_librarian
2. As this project requires using a database, for which we do not have a global server right now, you need to set up the database on your side. To do this, open the project in your favorite IDE or editor - we recommend Visual Studio; in VS, open Tools/NuGet Package Manager/Package Manager Console, and run the following command in the console: **update-database**. With this command, you will run the migration scripts provided by our team and set up a new database for this project.
3. Once you have the database, you will need to set the environment variables used in this project. Follow these steps on Windows:
   - windows button + R (or search for 'run' and open the Run app)
   - Copy this to the input field and hit ok: **rundll32 sysdm.cpl,EditEnvironmentVariables**
   - variable name : ConnectionStrings__databaseConnection
   - variable value : data source=localhost; initial catalog=WombatDb; integrated security=SSPI
3. Once the DB is all set up with the environment variables, hit "Run", or if you are familiar with the dotnet CLI, simply open the project folder in a command line tool, navigate into the WombatProjectfolder, and write **dotnet run** command
4. The backend runs now, but you cannot really use the webpage as it is supposed to be used with the frontend. In order to start the frontend too, please check out the above linked git repository, and follow the instructions there.
