FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY analysis-worker/src/. ./analysis-worker/src
COPY shared ./shared
COPY Directory.Packages.props ./

WORKDIR /src/analysis-worker/src
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "AnalysisWorker.dll"]
