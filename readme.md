# Tinyweb

Tinyweb is a lightweight web framework for ASP.NET that embraces HTTP and aims to be ridiculously simple.

## No Abstractions Please

Tinyweb moves away from the MVC pattern for developing web applications. Instead, you start with separate addressable resources that respond directly to the HTTP method that was used to access the resource. Every resource is a `class` and every implemented HTTP method (get, post, put, delete) is a method on the `class`. 

With Tinyweb, you're writing code that is conceptually close to the way HTTP actually works. *Your code and HTTP sitting in a tree, K.I.S.S.I.N.G.*

## Simplicity

Getting started is as simple as creating a resource (a `class` with a method) and calling `Tinyweb.Init()` from `Application_Start`. That's it. You don't even have to specify the resource URL (unless you want to), Tinyweb will infer it from the `class` name. View engines, model binding and all that goodness are supported too.

## Example

Create a handler:

    public class HelloWorldHandler
    {
    	public IHandlerResult Get()
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

## Resources

Links to blogs and other resources about Tinyweb:

[Introducing Tinyweb](http://invalidcast.com/2010/12/my-new-black)

[Tinyweb on OS X & Mono](http://invalidcast.com/2011/01/tinyweb-does-mono)

[ASP.NET - Thinking In Resources](http://invalidcast.com/2011/03/asp-net-thinking-in-resources)

## NuGet

The easiest way to get started with Tinyweb is to [NuGet it](http://nuget.org/List/Packages/Tinyweb):

    Install-Package Tinyweb

## Build Server

You can see the live build status (and grab the binary releases) from the [build server.](http://ci.thunder.invalidcast.com)

## Versions

1.0.1 Update for binding of array types, lower casing of routes and internal Spark integration refactoring

1.0.0 Release of stable public API and switch to semantic versioning
