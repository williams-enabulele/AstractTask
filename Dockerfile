#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app



FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.sln .


COPY ["AstractTask.Api/AstractTask.Api.csproj", "AstractTask.Api/"]
COPY ["AstractTask.Domain/AstractTask.Domain.csproj", "AstractTask.Domain/"]
COPY ["AstractTask.Infrastructure/AstractTask.Infrastructure.csproj", "AstractTask.Infrastructure/"]

RUN dotnet restore "AstractTask.Api/AstractTask.Api.csproj"
COPY . .

WORKDIR /src/AstractTask.Api
RUN dotnet build 

FROM build AS publish
WORKDIR /src/AstractTask.Api
RUN dotnet publish -c Release -o /src/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /src/publish .
ENV ASPNETCORE_URLS http://*:$PORT
ENTRYPOINT ["dotnet","AstractTask.Api.dll"]


