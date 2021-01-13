FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /
COPY . . 
RUN dotnet restore "Taco.WebApi/Taco.WebApi.csproj"
WORKDIR "/Taco.WebApi"
RUN dotnet build "Taco.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Taco.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Taco.WebApi.dll"]