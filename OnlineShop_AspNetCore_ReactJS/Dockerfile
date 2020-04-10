FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY . ./
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
ENTRYPOINT ["dotnet", "OnlineShop_AspNetCore_ReactJS.dll"]