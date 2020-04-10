FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

# Install Nodejs
RUN apt-get install --yes curl
RUN curl --silent --location https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install --yes nodejs

# Copy csproj and restore as distinct layers
WORKDIR /app
COPY OnlineShop_AspNetCore_ReactJS.csproj ./
RUN dotnet restore 

# Restore NPM dependencies
COPY ["ClientApp/package.json", "app/ClientApp/"]
COPY ["ClientApp/package-lock.json", "app/ClientApp/"]
WORKDIR /app/ClientApp
RUN npm install

# Copy everything else and build
WORKDIR /app
COPY . ./
RUN dotnet publish /app/OnlineShop_AspNetCore_ReactJS.csproj -c Release -o ./out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out/ .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
ENTRYPOINT ["dotnet", "OnlineShop_AspNetCore_ReactJS.dll"]