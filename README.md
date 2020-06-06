## Online Pie Shop (Asp .net Core, C#, SQLite, ReactJS, Bootstrap)

Online Pie Shop is a fictitious e-commerce application, that allows the pie shop company to sell pies online.

It is a work in progress and currently there are following pages:

    1. Home Page
    2. Pie Details page
    3. Pie List page
    4. Shopping Cart page
    5. About page

It has been built using following technology stack:

    1. Front end - ReactJS and Bootstrap
    2. Back end - REST / Web APIs using Asp.Net Core, C# and SQLite
    3. Azure board for work item creation and tracking
    4. Azure Repository (GIT) for maintaining the source code history
    5. Azure pipelines for running continuous integration build
    6. Google cloud build for running continuous deployment build, to Google cloud app engine (PAAS)
    7. Docker / Containers
    8. Test driven development / Unit testing / Integration testing

## Steps to set up code locally

There are two ways to run the code locally:

1. With Docker, which requires only [Docker](https://docs.docker.com/) to be installed.

2. Without Docker, which requires [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1), [NodeJS runtime / executable](https://nodejs.org/) and [VS Code](https://code.visualstudio.com/) or any code editor of your choice.

3. Install [Git](https://git-scm.com/downloads) and clone the code locally, execute `git clone https://github.com/sethiharish/OnlineShop_AspNetCore_ReactJS.git`

## To Run with Docker

1. Open Bash (Linux shell) and navigate to code folder where Dockerfile is present, execute `cd OnlineShop_AspNetCore_ReactJS/OnlineShop_AspNetCore_ReactJS/`

2. Build docker image, execute `docker build . -t onlineshop_aspnetcore_reactjs`

3. Run docker container, execute `docker run -d -p 8080:8080 onlineshop_aspnetcore_reactjs`

4. Open the browser and navigate to [http://localhost:8080/](http://localhost:8080/)

5. To test the REST Apis navigate to [SwaggerUI](http://localhost:8080/swagger/index.html)

## Run using Docker Compose

Prerequisite:

1. Make sure that docker componse is installed on the host. Refer to the [link](https://docs.docker.com/compose/install/) for installation.

Steps:

1. Open Bash (Linux shell) and navigate to code folder where Dockerfile is present, execute `cd OnlineShop_AspNetCore_ReactJS/OnlineShop_AspNetCore_ReactJS/`

2. execute `docker-compose up -d`

3. Open the browser and navigate to [http://localhost:8080/](http://localhost:8080/)

4. To test the REST Apis navigate to [SwaggerUI](http://localhost:8080/swagger/index.html)

5. This also runs the mongodb container which is exposed on the default mongo db port on the host.

## To Run without Docker

1. Navigate to ClientApp folder, execute `cd OnlineShop_AspNetCore_ReactJS\OnlineShop_AspNetCore_ReactJS\ClientApp`

2. Restore NPM packages, execute `npm install`

3. Navigate one folder up, execute `cd..` to `\OnlineShop_AspNetCore_ReactJS\OnlineShop_AspNetCore_ReactJS\`

4. Restore Nuget packages, execute `dotnet restore`

5. Deployment mode (client &amp; server are running on same PORT / under same process):

   5.1. Publish the project to generate executables, execute `dotnet publish -c Release`

   5.2. Navigate to publish folder, execute `cd bin\MCD\Release\netcoreapp3.1\publish\`

   5.3. Run the application, execute `dotnet OnlineShop_AspNetCore_ReactJS.dll`

6. Development mode (client &amp; server are running on separate PORT / under separate processes):

   6.1. Navigate one folder up, execute `cd..` to `\OnlineShop_AspNetCore_ReactJS\`

   6.2. Open the project `OnlineShop_AspNetCore_ReactJS` in VS code, execute `code .`

   6.3. Open the terminal and navigate to ClientApp folder, execute `cd OnlineShop_AspNetCore_ReactJS\ClientApp`, execute `npm start` to start the client

   6.4. Open another terminal and navigate to folder where `OnlineShop_AspNetCore_ReactJS.csproj` file is present, execute `cd OnlineShop_AspNetCore_ReactJS\`, to run the server / application, execute `dotnet run`

7. Open the browser and navigate to [https://localhost:5001/](https://localhost:5001)

8. To test the REST Apis navigate to [SwaggerUI](https://localhost:5001/swagger/index.html)

It is a work in progress and being improved continously, in case you see any issues, please let me know.
