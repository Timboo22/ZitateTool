# --- STAGE 1: Angular Build ---
FROM node:22-alpine AS angular-build
WORKDIR /app

# Da Docker in 'src/backend' startet, müssen wir 
# eine Ebene höher gehen. Das geht NUR, wenn der 
# Build Context in Dokploy auf 'src' oder '/' steht!
COPY ../frontend/package*.json ./
RUN npm install
COPY ../frontend/ ./
RUN npm run build -- --configuration=production

# --- STAGE 2: .NET Build ---
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Pfade relativ zum Ordner 'src/backend'
COPY Sanoa.Api/Sanoa.Api.csproj Sanoa.Api/
RUN dotnet restore Sanoa.Api/Sanoa.Api.csproj
COPY Sanoa.Api/ Sanoa.Api/
WORKDIR /src/Sanoa.Api

# Frontend in wwwroot kopieren
COPY --from=angular-build /app/dist/sanoa-frontend/browser ./wwwroot

RUN dotnet publish -c Release -o /app/publish

# --- STAGE 3: Final ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Sanoa.Api.dll"]
