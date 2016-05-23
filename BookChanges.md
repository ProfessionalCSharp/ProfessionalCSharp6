This document contains changes with .NET Core that have been changed after the book **Professional C# 6 and .NET Core 1.0** was published, as well as typos.

## Chapter 1 - .NET Application Architectures

Page 15 - .NET Native
.NET Native didn't make it to the first release of .NET Core. Building native applications on Linux and Windows will be possible with a later release. Currently it's just possible to compile UWP applications to native code.

Page 19 - Note
The option --type will not be available using *dotnet new*. Instead, a much more flexible option will be available with preview 2 of the tools. 
Preview 2 offers the --template option to select any template. Installed templates will be shown wiht dotnet new --list. See [Reimagine dotnet-new](https://github.com/dotnet/cli/issues/2052)

Page 20 - The *compilationOptions* from project.json changed to *buildOptions

## Chapter 6 - Generics

Page 173 - Typo in the first note
Within the method GetRectangles an underscore is missing accessing the variable _coll

## Chapter 24 - Security

Page 697, the InitProtection method changed because of API changes with data protection:
```
public static MySafe InitProtection()
{
  var serviceCollection = new ServiceCollection();   
  serviceCollection.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("."))
    .SetDefaultKeyLifetime(TimeSpan.FromDays(20))
    .ProtectKeysWithDpapi();
          
  IServiceProvider services = serviceCollection.BuildServiceProvider();

  return ActivatorUtilities.CreateInstance<MySafe>(services);
}
```

## Chapter 38 - Entity Framework Core

Page 1169, tools section:
The Entity Framework Core tools need to be referenced using "Microsoft.EntityFrameworkCore.Tools" instead of "dotnet-ef":

```
"tools": {
  "Microsoft.EntityFrameworkCore.Tools": {
    "version": "1.0.0-*",
    "imports": "portable-net452+win81"
  }
}
```

## Chapter 40 - ASP.NET Core

Page 1227, the Main method changed slightly with UseKestrel (the new Web host), UseIISIntegration (integration when used with IIS), and UseContentRoot (to define the static content for the Web site):

```
public static void Main(string[] args)
{
  var host = new WebHostBuilder()
    .UseKestrel()
    .UseIISIntegration()
    .UseContentRoot(Directory.GetCurrentDirectory())
    .UseStartup<Startup>()
    .Build();

  host.Run();
}
```

Page 1250, the directory for the configuration is now configured with SetBasePath

```
public Startup(IHostingEnvironment env)
{
  var builder = new ConfigurationBuilder()
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

  //...
}
```
