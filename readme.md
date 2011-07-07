# Tinyweb

Tinyweb is a lightweight web framework for ASP.NET that embraces HTTP and aims to be ridiculously simple.

## No Abstractions Please

Tinyweb moves away from the MVC pattern for developing web applications. Instead, you start with separate addressable resources that respond directly to the HTTP method that was used to access the resource. Every resource is a `class` and every implemented HTTP method (get, post, put, delete) is a method on the `class`. 

With Tinyweb, you're writing code that is conceptually close to the way HTTP actually works.

## Simplicity

Getting started is as simple as creating a resource (a `class` with a method) and calling `Tinyweb.Init()` from `Application_Start`. That's it. You don't even have to specify the resource URL (unless you want to), Tinyweb will infer it from the `class` name. View engines, model binding and all that goodness are supported too.

## Example

Create a handler:

    public class HelloWorldHandler
    {
    	public IResult Get()
    	{
    		return View.Spark("Hello.spark");
    	}    	
    }

You now have a resource that supports HTTP GET at the location /hello/world and returns a rendered Spark template. Let's tell Tinyweb to load itself and our work here is done.

    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Tinyweb.Init();
        }
    }
	
## It's Still ASP.NET

Tinyweb is just another web framework, but underneath it's still the same ASP.NET you've been using for years. You still have access to `Request`, `Response`, `Session`, `Cache` and all of that other stuff that makes you squeal in delight.

## Documentation

Details of all features of the framework and how to use them can be found in the [docs](https://github.com/martinrue/Tinyweb/wiki). 

## Resources

Links to blogs and other resources about Tinyweb:

[Introducing Tinyweb](http://invalidcast.com/2010/12/my-new-black)

[Tinyweb on OS X & Mono](http://invalidcast.com/2011/01/tinyweb-does-mono)

[ASP.NET - Thinking In Resources](http://invalidcast.com/2011/03/asp-net-thinking-in-resources)

[Tinyweb Series: 1 Getting Started](http://invalidcast.com/2011/05/tinyweb-series-1-getting-started)

[Tinyweb Series: 2 Building APIs](http://invalidcast.com/2011/05/tinyweb-series-2-building-apis)

[Tinyweb Series: 3 Dependency Injection & Filters](http://invalidcast.com/2011/05/tinyweb-series-3-dependency-injection-filters)

[Tinyweb Series: 4 Views & Model Binding](http://invalidcast.com/2011/05/tinyweb-series-4-views-model-binding)

## Applications

The following full applications have been built with Tinyweb:

[Tweet Conversations](http://tweetconversations.com)

[Sentiment Analysis Demo](http://sentiment.brandlisten.com)

## NuGet

The easiest way to get started with Tinyweb is to [NuGet it](http://nuget.org/List/Packages/Tinyweb):

    Install-Package Tinyweb

## Build Server

You can see the live build status (and grab the binary releases) from the [build server.](http://ci.thunder.invalidcast.com)

## Versions

2.1.5 Fix for Url.For helper to include the virtual application path

2.1.4 Fixed bug with Result.JsonOrXml when no accept header is specified - it now defaults to JSON

2.1.3 Fixed lazy loading bug that caused init to load handlers twice during scanning

2.1.2 Added `Ignore` attribute for excluding specific properties of a class from model binding

2.1.1 Fixed bug caused by `Result.Redirect` allowing `ThreadAbort` exceptions to flow through to the `Tinyweb.OnError` delegate for every redirect

2.1.0 Added support for an error handler for managing errors thrown during handler/filter execution and added access to `HandlerData` to model binding

2.0.1 Fixed bug that prevented handlers accessing session state from the `RequestContext`

2.0.0 Support for global and handler filters for before/after processing and changed public API by renaming `IHandlerResult` to `IResult`, forcing semver v2.0.0

1.0.2 When `Tinyweb.AllowFormatExtensions` is set, `Result.JsonOrXml` allows the use of a URL override for specifying the requested content type (i.e. /resource.json)

1.0.1 Update for binding of array types, lower casing of routes and internal Spark integration refactoring

1.0.0 Release of stable public API and switch to semantic versioning
