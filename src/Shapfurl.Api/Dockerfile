#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src
COPY ["src/Shapfurl.Api/Shapfurl.Api.fsproj", "src/Shapfurl.Api/"]
RUN dotnet restore "src/Shapfurl.Api/Shapfurl.Api.fsproj"
COPY . .
WORKDIR "/src/src/Shapfurl.Api"
RUN dotnet build "Shapfurl.Api.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shapfurl.Api.fsproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shapfurl.Api.dll"]