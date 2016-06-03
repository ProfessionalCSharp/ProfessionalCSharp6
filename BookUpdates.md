This document contains changes with .NET Core that have been changed after the book **Professional C# 6 and .NET Core 1.0** was published, as well as typos.

## Chapter 1 - .NET Application Architectures

Page 15 - .NET Native
.NET Native didn't make it to the first release of .NET Core. Building native applications on Linux and Windows will be possible with a later release. Currently it's just possible to compile UWP applications to native code.

Page 19 - Note
The option --type will not be available using *dotnet new*. Instead, a much more flexible option will be available with preview 2 of the tools. 
Preview 2 offers the --template option to select any template. Installed templates will be shown wiht dotnet new --list. See [Reimagine dotnet-new](https://github.com/dotnet/cli/issues/2052)

Page 20 - The *compilationOptions* from project.json changed to *buildOptions

Page 20 - The framework *netstandardapp1.5* has been changed to "netcoreapp1.0"

Page 20: - using preview 1 of the dotnet tools, *dotnet new* produces this *project.json*:

```
{
  "version": "1.0.0-*",
  "buildOptions": {
    "emitEntryPoint": true
  },
  "dependencies": {
    "Microsoft.NETCore.App": {
      "type": "platform",
      "version": "1.0.0-rc2-3002702"
    }
  },
  "frameworks": {
    "netcoreapp1.0": {
      "imports": "dnxcore50"
    }
  }
}
```

The dependency to *Microsoft.NETCore.App* also includes a configuration for the runtimes. With this configuration, *dotnet publish* doesn't work correctly to produce an executable, and you also cannot use this dependency to also create .NET 4.6 builds. You can change *project.json* to this to correctly produce an executable with *dotnet publish* (change to *NETStandard.Library*, and add the *runtimes* section as also shown in the book:

```
{
  "version": "1.0.0-*",
  "buildOptions": {
    "emitEntryPoint": true
  },
  "dependencies": {
	  "NETStandard.Library": "1.5.0-*"
  },
  "frameworks": {
    "netcoreapp1.0": {
      "imports": "dnxcore50"
    }
  },
  "runtimes": {
    "win7-x64": { }
  }
}

```

This also allows adding direct support to build a .NET 4.6 binary:

```
{
  "version": "1.0.0-*",
  "buildOptions": {
    "emitEntryPoint": true
  },
  "dependencies": {
	  "NETStandard.Library": "1.5.0-*"
  },
  "frameworks": {
    "netcoreapp1.0": {
      "imports": "dnxcore50"
    },
    "net46": {
    }
  },
  "runtimes": {
    "win7-x64": { }
  }
}
```

Page 20 - Version 1.4 of NetStandard.Library now includes support for the Universal Windows Platform (UAP). See the [.NET Platform Standard](https://github.com/dotnet/corefx/blob/master/Documentation/architecture/net-platform-standard.md ".NET Platform Standard").
New is version 1.6 which includes support for .NET 4.6.3 and .NET Core 1.0.

Page 22 - Note, inside the note *dotnet compile* is mentioned. This should be *dotnet build

Page 23 - the table lists *7.0* for Entity Framework. It should be *Core 1.0* instead

## Chapter 3 - Objects and Types

Page 76 - typo: *_* missing with _firstName variable within get accessor

Page 84 - typo: new MySingleton(42) should by new Singleton(42)

## Chapter 6 - Generics

Page 173 - Typo in the first note
Within the method GetRectangles an underscore is missing accessing the variable _coll

## Chapter 16 - Reflection, Metadata, and Dynamic Programming

Page 437 - CompilationOptions has been changed to BuildOptions

Page 437/438 - Loading an assembly dynamically has become easier, the DirectoryLoader is no longer needed, use AssemblyLoadContext instead of PlatformServices

```
private static object GetCalculator()
{
  Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(CalculatorLibPath);
  Type type = assembly.GetType(CalculatorTypeName);
  return Activator.CreateInstance(type);
}
```

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

Page 1166, the EF context with depdendency injection now needs a constructor with DbContextOptions<TContext>:

```
public class BooksContext : DbContext
{
  public BooksContext(DbContextOptions<BooksContext> options)
    : base(options)
  {          
  }
  public DbSet<Book> Books { get; set; }
}
```

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

## Chapter 41 - ASP.NET MVC

Page 1272, ToString instead of ToShortDateString:

```
<td>@item.Day.ToString("d")</td>
```

Page 1299, 1300
Action result names have been changed, the Http prefix removed: HttpBadRequest, HttpNotFound... renamed to BadRequest, NotFound.
[Action result naming changes](https://github.com/aspnet/Announcements/issues/153)

