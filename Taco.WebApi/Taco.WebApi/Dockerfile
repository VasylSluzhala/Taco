#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Taco.WebApi/Taco.WebApi.csproj", "Taco.WebApi/"]
RUN dotnet restore "Taco.WebApi/Taco.WebApi.csproj"
COPY . .
WORKDIR "/src/Taco.WebApi"
RUN dotnet build "Taco.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Taco.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Taco.WebApi.dll"]