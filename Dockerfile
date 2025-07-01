# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copie de la solution et des projets
COPY Portfolio-Machado.sln ./
COPY Portfolio/Client/ ./Portfolio/Client/
COPY Portfolio/Server/ ./Portfolio/Server/
COPY Portfolio/Shared/ ./Portfolio/Shared/

# Restauration des dépendances
WORKDIR /app/Portfolio/Server
RUN dotnet restore

# Publication du projet en mode Release
RUN dotnet publish -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# Copie les fichiers publiés
COPY --from=build /out ./

# Point d’entrée
ENTRYPOINT ["dotnet", "Portfolio.Server.dll"]
