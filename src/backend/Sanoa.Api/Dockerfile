# --- STAGE 1: Angular Build ---
FROM node:22-alpine AS angular-build
WORKDIR /frontend-src

COPY frontend/package*.json ./
RUN npm install
COPY frontend/ ./

RUN npm run build -- --configuration=production

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["backend/Sanoa.Api.csproj", "./"]
RUN dotnet restore "Sanoa.Api.csproj"
COPY backend/ . 

# --- HIER IST DIE MAGIE: Angular in wwwroot schieben ---
# Ersetze 'sanoa-frontend' durch den Namen aus deiner angular.json
COPY --from=angular-build /frontend-src/dist/sanoa-frontend/browser ./wwwroot

RUN dotnet build "Sanoa.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Sanoa.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# --- STAGE 4: Final Stage ---
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sanoa.Api.dll"]
