---
tags:
- docker
- asp.net core
- tutorial
---

In this post, we will look at building ASP.NET Core Docker App. We will first examine how to do this without Visual Studio and then with Visual Studio Tools for Docker.

## Prerequisites

- Docker for Windows
- Visual Studio 2017 with .NET Core cross-platform development Workloads

# Without Visual Studio 2017

ASP.NET Core app can be coded entirely without IDE. In this section, we will examine how to dockerize a ASP.NET Core app.

## Sample ASP.NET Core App

Open a command prompt and run the following command to create and test a sample ASP.NET Core app.

``` bat
REM create a folder named 'HelloWorld'
mkdir HelloWorld

REM navigate into the newly created folder
cd HelloWorld

REM create a new ASP.NET Core console app based on the name of the folder
dotnet new webapi

REM run the newly created ASP.NET Core console app
dotnet run
```

We will see that the application is now listening on ```http://localhost:5000```. Open your favorite web browser and navigate to the following url ```http://localhost:5000/api/values```. Since we are running a web api, you should see a ```["value1","value2"]```.

## Dockerize ASP.NET Core App

To dockerize the .NET Core 2 app, we will create a `Dockerfile`, with no extension, and save it in the `HelloWorld` directory with the following content. Dockerfile contains Docker build instructions that runs in sequence.

``` docker
# FROM instruction, which must be first, initializes a new build stage and sets the Base Image for the remaining instructions.
FROM microsoft/dotnet:2.0-sdk

# WORKDIR sets the working directory of any remaining RUN, CMD, ENTRYPOINT, COPY and ADD instruction.
WORKDIR /app

# COPY copies the csproj to the container.
COPY *.csproj ./

# RUN executes commands in a new layer on top of the current image and commit the results. In this case, we get the needed dependencies of the project.
RUN dotnet restore

# COPY copies the rest of the files into our container into new layers.
COPY . ./

# RUN executes the command to publish the app into the out directory.
RUN dotnet publish -c Release -o out

# ENTRYPOINT instruction allows the container to run as an executable.
ENTRYPOINT ["dotnet", "out/HelloWorld.dll"]
```

## Build and Run the Docker App

With the dockerfile ready, we can execute the following commands to build and run the container.

``` dos
docker build -t dotnetapp-dev .
docker run --rm dotnetapp-dev HelloWorld from Docker
```

# Using Visual Studio 2017

Visual Studio 2017 provides support for Docker via Visual Studio Tools for Docker.

## Create Sample .NET Core App

In Visual Studio 2017, create a .NET Core Console app. Run the app and you should see an output with ```Hello World!```.

## Add Docker Support

Adding docker support in Visual Studio is extremely easily with Visual Studio Tools for Docker.

- Right click the project, select ```Add``` and ```Docker Support```
- On the ```Docker Support Options``` prompt, select the proper target OS whether you are running ```Windows``` or ```Linux``` containers.

This would add a solution folder called ```docker-compose``` and several docker related files.

## Build and Run the Docker App

With the ```docker-compose``` as StartUp project, simply run the application and you should see an output with ```Hello World!```.
