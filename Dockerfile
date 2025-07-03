# syntax=docker/dockerfile:1

#FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
#WORKDIR /app
#EXPOSE 80
#
#FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
#WORKDIR /src
#
## Copy all project files
#COPY . .
#
## Restore dependencies
#RUN dotnet restore HomeSecurityCRUD.csproj
#
#
## Build
#RUN dotnet restore HomeSecurityCRUD.csproj -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=build /app/publish .
#
#ENTRYPOINT ["dotnet", "HomeSecurityCRUD.dll"]
#
#

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["HomeSecurityCRUD.csproj", "."]
RUN dotnet restore "./YourProject.csproj"
COPY . .
RUN dotnet build "HomeSecurityCRUD.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HomeSecurityCRUD.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeSecurityCRUD.dll"]