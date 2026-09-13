FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY TemperatureApi.csproj ./
RUN dotnet restore TemperatureApi.csproj

COPY . ./
RUN dotnet publish TemperatureApi.csproj -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

ENV ASPNETCORE_URLS=http://+:8081
EXPOSE 8081

USER $APP_UID

ENTRYPOINT ["dotnet", "TemperatureApi.dll"]
