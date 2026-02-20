# --- STAGE 1: Angular Build ---
FROM node:22-alpine AS angular-build
WORKDIR /app
# Wir gehen zwei Ebenen hoch, um src/frontend zu erreichen
COPY src/frontend/package*.json ./ 
# (Hinweis: Wenn Dokploy den Context auf Root setzt, ist dieser Pfad richtig)
RUN npm install
COPY src/frontend/ ./
RUN npm run build -- --configuration=production

# --- STAGE 2: .NET Build ---
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
# Pfad zur csproj
COPY src/backend/Sanoa.Api/Sanoa.Api.csproj src/backend/Sanoa.Api/
RUN dotnet restore src/backend/Sanoa.Api/Sanoa.Api.csproj
COPY src/backend/Sanoa.Api/ src/backend/Sanoa.Api/
WORKDIR /src/src/backend/Sanoa.Api

# Kopiere Angular in den wwwroot (Name prüfen!)
COPY --from=angular-build /app/dist/sanoa-frontend/browser ./wwwroot

RUN dotnet publish -c Release -o /app/publish

# --- STAGE 3: Final ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Sanoa.Api.dll"]
