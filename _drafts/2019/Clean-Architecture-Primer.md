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
These workflows make use of the domain logics. These workflows reflect the functional use case of the application. 
Application should have CQRS so it is a transaction script type pattern. With CQRS, each feature in it own class.
There are 3 types of CQRS: Single Database, Two Database (Read and Write Split), Event Sourcing (Read Database and Event Store, much more complexe and beyond scope, audit trail, point-in-time reconstruction, replay events, etc.)
Furthermore, with CQRS, we can optimize the code for read and for write easily because they are separated. Therefore, the optimization can be in query, in code or in architecture. For example, queries can bypass the domain to speed up everything.
Moving away for domain services that have a huge collection of related operations. So breaking the domain services into bits.
The only thing I don't like so far is the ViewModel through to the client. Because it leaves the application and goes th   rough the UI Application layer should accept request object and return response object. Then at the distribution layer, it is transformed into a ViewModel. Copy the eShopOnWeb for this.
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

Clean Architecture with Microservices

Bounded Domain Context
Each microservice has their own domain context. If a customer means different things then it's probably a different domain context. Microservices architecture subdivide monoliths, clearly-defined interfaces, small teams, independent deployment and very simliar to SOA. One service in SOA can be built with a microservice architecture. Inidividual service got a bounded context, high cohesion, low compling, single domain of knowledge, consistent data model, independent technology. The size and scope of each service depends. Sometimes, we go by aggregate roots and less. There is bound to be communication between the services. If there is too much, then maybe bring them together. Service that communicate in between can send entity but only one service is owner of the entity. Also, the identifier is the same in both service.

Clean Architecture as Testable Architecture
In test driven development, we do red, green refactor. Create failing test, get test to pass, and refactor to improve the code. As such, the tests are being built as we go and the architecture is testable from the start. Testable architecture is key to clean architecture.
Test pyramid: unit tests (bottom), service tests (middle), ui tests (top) and manual. Service are like functional.

Final Thoughts: Evolving the Architecture

Clean architecture focus on the problem at hand and the business logic and the use cases. So it defer the implementation details like database and ui to later on. 

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

Dependency Injection
Dependency injection should be at different level. Each modules can expose a dependency map. The startup will basically call each of those map to link them together at the root.
https://github.com/jonathandotchin/CQRSCleanArchitecture/blob/master/src/WebUI/Startup.cs
Note how the startup calls add application and add infrastructure. Basically, he extends iservicescollection with extra methods

Custom exception handler
Translate application/domain exception into http error code
https://github.com/jonathandotchin/NorthwindTraders/blob/master/Src/WebUI/Common/CustomExceptionHandlerMiddleware.cs
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-3.1
The order in the startup.cs matters.

Organize Code by Functions
Also, when we organize the content of our application, we shouldn't do it based on the components of the application itself (like controllers, models, views) but more by the feature/functionality. It makes things much easier to find. In functional organization, we have spatial locality, what is used together is found together but we might lose automation and convention from the framework. For example, the application project will have folders for the aggregate root object because they represent the different entry point for each use cases. What's good is when you are debugging something that is wrong with your customer query, you need it is in the customer zone and you can start digging whereas you are digging through controllers and views. In a way, we are already doing it in the API.