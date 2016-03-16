ReadMe - Code Samples for Chapter 38, Entity Framework Core

The sample code for this chapter contains this solution:
- EntityFrameworkSamples

Most of the projects of this solution are .NET Core projects. The only project using the full framework is MenusSampleMSBuild to show using the MSBuild based migration commands.

The database that is used with many of these samples is the Books database. Look for the backup file Books.bak. You can use this file to restore the Books database using SQL Server Management Studio.
The Menus database that is also used in some samples is created using the Migrations feature of Entity Framework.

To open and build the .NET Core projects of Networking, you need to install ASP.NET 5 tools in addition to Visual Studio 2015. 
Install the tools from here:
https://go.microsoft.com/fwlink/?LinkId=627627

After opening the files from Visual Studio, you might need to open the command prompt and download the necessary NuGet packages using
>dnu restore

If you have the .NET Core Command Line (CLI) Tools installed, you can use
>dotnet restore
instead.

Please download and install the .NET Core Command Line (CLI) Tools from https://github.com/dotnet/cli. For Windows, you will find an MSI package that you can install on your Windows system: https://dotnetcli.blob.core.windows.net/dotnet/beta/Installers/Latest/dotnet-win-x64.latest.exe


The downloadable code is based on RC1 of .NET Core. Mainly the NuGet packages as well as the namespaces will change. For example, the NuGet package EntityFramework.Core will be replaced by Microsoft.EntityFrameworkCore, and the namespace Microsoft.Data.Entity by Microsoft.EntityFrameworkCore. 

When .NET Core RC2 is available, the files from this Chapter will be changed. With all projects, the project.json files will be changed, as well as the namespaces and a few code lines in the C# source files.

Please re-check the Wrox code downloads for updates.

Currently,
8-Apr is planned for RC2
30-Jun is planned for RTM of .NET Core.
Please check this link for Microsoft's actual schedule:
https://github.com/dotnet/corefx/milestones

For code comments and issues please check https://github.com/ProfessionalCSharp/ProfessionalCSharp6

Thank you!
Christian