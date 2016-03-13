ReadMe - Code Samples for Chapter 28, Localization

The sample code for this chapter contains these solutions:
- Localization
- Net46Localization

The .NET Core projects from Localization are duplicated in Net46Localization with full .NET Framework projects. The source code files are linked from the Localization project.
Some of the projects from Localization are not duplicated:
- CustomCultures: this is a .NET Framework project, as the CultureAndRegionInfoBuilder is not available with .NET Core
- UWPLocalization (a UWP project)
- WebApplicationSample (a ASP.NET Core project)
- WPFApplication (a WPF application)

You need Windows 10 to run the UWP app. For localizing the UWP application, install the Multilingual App Toolkit v4.0 (via Visual Studio 2015, Extensions and Updates).

The WebApplicationSample will be changed when RC2 is available. Expect several code changes with ASP.NET, as you can already see in the code comments.

To open and build the .NET Core projects of Localization, you need to install ASP.NET 5 tools in addition to Visual Studio 2015. 
Install the tools from here:
https://go.microsoft.com/fwlink/?LinkId=627627 

After opening the files from Visual Studio, you might need to open the command prompt and download the necessary NuGet packages using
>dnu restore

If you have the .NET Core Command Line (CLI) Tools installed, you can use
>dotnet restore
instead.

Please download and install the .NET Core Command Line (CLI) Tools from https://github.com/dotnet/cli. For Windows, you will find an MSI package that you can install on your Windows system: https://dotnetcli.blob.core.windows.net/dotnet/beta/Installers/Latest/dotnet-win-x64.latest.exe

When .NET Core RC2 is available, the files from this Chapter will be changed. With all projects, the project.json files will be changed. C# source code will change as well (as you already can read from the comments).

Please re-check the Wrox code downloads for updates.

Currently,
8-Apr is planned for RC2
30-Jun is planned for RTM of .NET Core.
Please check this link for Microsoft's actual schedule:
https://github.com/dotnet/corefx/milestones

For code comments and issues please check https://github.com/ProfessionalCSharp/ProfessionalCSharp6

Thank you!
Christian