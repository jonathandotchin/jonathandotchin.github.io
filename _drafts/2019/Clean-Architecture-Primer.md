Clean Architecture based on Onion Architecture based on Hexagone Architecture

Core == (Application Logic(Domain Logic))

Domain Logic == Business logic that can be shared by multiple applications.
In domain logic, you can find value object. Value object are immutable.
Domain layer in our case is fairly small because we have few domain rules. We should avoid data annotation (keep entity clean, entity map not domain but in infrastructure), use value object, initialize all collections & private set, custom exception and track changes with auditableentry.
IDateTime (interface) is in domain to avoid dependency on system clock because it's infrastructure (implementation).
To avoid data annotation. In entity framework, we can use those data map and fluent mapping instead.
EF Core Fluent Configuration for Model to Database mapping is correct. Because it's out of the domain and data annotation is limited anyway.
It should make up mainly for value object and aggregate root.

Application Logic == Business logic that is proper to a single application.
In the application logic, you can find the use cases. The workflows.
These workflows make use of the domain logics.
Application should have CQRS so it is a transaction script type pattern. With CQRS, each feature in it own class.
Moving away for domain services that have a huge collection of related operations. So breaking the domain services into bits.
The only thing I don't like so far is the ViewModel through to the client. Because it leaves the application and goes through the UI Application layer should accept request object and return response object. Then at the distribution layer, it is transformed into a ViewModel. Copy the eShopOnWeb for this.
We are using the request/response pattern so I would called the input *Request and the output *Response. https://github.com/jasontaylordev/CleanArchitecture/issues/3

(Infrastructure(Application(Domain))Distribution)

Infrastructure contains all external concerns and we should be able to swap them. Feel free to create sub project or namespace for data, logging, identity, file system, etc.
Infrastructure keypoints: indepedent from database, fluent api configuration over data annotation, conventions over configuration (less code)
EF Core has features to unit tests but it's limited. But the idea is to use a real database instead of mocking everything. Configuring your mocks/stubs vs. configuring your db is the same time.
Unit of work + repository? when used correctly, ef core hide db changes, dbcontext is unit of work, dbset is repository.
Repository with aggregate root is a good reason to implement it. Also saving a person but don't care about db saving implementation.
With the above you can basically map and use entity splitting https://www.c-sharpcorner.com/article/table-splitting-in-entity-framework-2-0/

Distribution contains the distribution of our application either via WebUI, API and the like.
Custom middleware can be used here to convert custom exception to proper http code for web api.
Presentation/Distribution layer is the client SPA, Web API, MVC or anything.
Web UI depends on infrastructure but only in the startup.cs which is the root of the application.
Controller should haven't any logic. It calls CQRS but who builds the DTOs that goes to client? I am hesitent to say the application layer. But the application should decide if a button is disable or not. The controller should transform the returned response into something for the client. Taking into account the version and all.
WebUI: controllers should not contains application logic, create and consume viewmodels, client just databind, use openapi (generate client) with nswag

Useful Library

Library called MediatR is useful because it wraps input and output into request/response data transfer object. We can use it to attached logging and validation.
Example of logging is the pre and post processing at https://github.com/jonathandotchin/NorthwindTraders/blob/master/Src/Application/Common/Behaviours/RequestLogger.cs. Don't forget to register them. In ASP.NET Core, the library offers a facilitator. If you're using ASP.NET Core then you can skip the configuration and use MediatR's MediatR.Extensions.Microsoft.DependencyInjection package which includes a IServiceCollection.AddMediatR(Assembly) extension method, allowing you to register all handlers and pre/post-processors in a given assembly. https://github.com/jbogard/MediatR.Extensions.Microsoft.DependencyInjection/blob/master/src/MediatR.Extensions.Microsoft.DependencyInjection/Registration/ServiceRegistrar.cs

Auto Mapper @ 23:30 with default implementation for interface that will handle all those conventions easy breezy and you need to implement the specific.
https://github.com/jonathandotchin/CQRSCleanArchitecture/blob/master/src/Application/TodoLists/Queries/GetTodos/TodoItemDto.cs
We got convention based mapping and if it is not possible with convention, the mapping function handles the rest. For example, the priority is from enum to int.

Validation is inside its own class. Could be application and/business. Useful library would be FluentValidation
https://docs.fluentvalidation.net/en/latest/index.html

Dependency flows inward of the onion. Use dependency injection. The default one from Microsoft/ASP.Net Core seems fine but people seems to enjoy AutoFac.
It is, however, not the most performant once. https://www.palmmedia.de/Blog/2011/8/30/ioc-container-benchmark-performance-comparison

Useful technique

Dependency injection should be at different level. Each modules can expose a dependency map. The startup will basically call each of those map to link them together at the root.
https://github.com/jonathandotchin/CQRSCleanArchitecture/blob/master/src/WebUI/Startup.cs
Note how the startup calls add application and add infrastructure. Basically, he extends iservicescollection with extra methods

Custom exception handler
Translate application/domain exception into http error code
https://github.com/jonathandotchin/NorthwindTraders/blob/master/Src/WebUI/Common/CustomExceptionHandlerMiddleware.cs
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-3.1
The order in the startup.cs matters.
