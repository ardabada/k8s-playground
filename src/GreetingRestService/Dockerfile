FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GreetingRestService/GreetingRestService.csproj", "GreetingRestService/"]
RUN dotnet restore "GreetingRestService/GreetingRestService.csproj"
COPY . .
WORKDIR "/src/GreetingRestService"
RUN dotnet build "GreetingRestService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GreetingRestService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
EXPOSE 443
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "GreetingRestService.dll"]