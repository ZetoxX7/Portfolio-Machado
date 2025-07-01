# Utilise l'image SDK .NET 7 officielle pour builder
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copie les fichiers projet et restaure les dépendances
COPY *.sln .
COPY Portfolio.Server/ Portfolio.Server/
COPY Portfolio.Shared/ Portfolio.Shared/
COPY Portfolio.Client/ Portfolio.Client/

RUN dotnet restore

# Build l’application
WORKDIR /app/Portfolio.Server
RUN dotnet publish -c Release -o /out

# Utilise l'image runtime .NET 7 pour exécuter l’app
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# Définit le point d'entrée
ENTRYPOINT ["dotnet", "Portfolio.Server.dll"]
