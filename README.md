Elmah.SqlServer.EFInitializer
=============================

**_This project is very young! Please do raise any issues that you find and I will try and resolve them._**

Automatically creates Elmah database on SQL Server using Entity Framework migrations.

#### Post-Install Actions
1. Add a call to `ElmahContext.Initialize()` in start-up code (e.g. in Global.asax)

**_NOTE!_**
The intent of this package is _only_ to automate the creation of a backing SQL Server database for Elmah.
You will also need to install something like the main [Elmah](https://www.nuget.org/packages/elmah/) package or [Elmah.MVC](https://www.nuget.org/packages/Elmah.MVC/) to be able to actually use error logging.

Credit must go to the creators of the [elmah.sqlserver](https://www.nuget.org/packages/elmah.sqlserver/1.2.0) package, the SQL from that project is what I used for the migrations.
