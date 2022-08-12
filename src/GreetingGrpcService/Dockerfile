FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GreetingGrpcService/GreetingGrpcService.csproj", "GreetingGrpcService/"]
RUN dotnet restore "GreetingGrpcService/GreetingGrpcService.csproj"
COPY . .
WORKDIR "/src/GreetingGrpcService"
RUN dotnet build "GreetingGrpcService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GreetingGrpcService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 5000
EXPOSE 5001
EXPOSE 443
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "GreetingGrpcService.dll"]