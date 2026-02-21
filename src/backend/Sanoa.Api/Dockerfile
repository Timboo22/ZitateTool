# Basis-Image fuer die Runtime (ARM64 kompatibel)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# SDK-Image fuer den Build
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG TARGETARCH
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Kopiere csproj und restore
COPY *.csproj ./
RUN dotnet restore -a $TARGETARCH

# Kopiere alles
COPY . .

# Baue die App und stelle sicher, dass wwwroot mitkommt
RUN dotnet publish *.csproj -c $BUILD_CONFIGURATION -o /app/publish -a $TARGETARCH --self-contained false

# Finales Image
FROM base AS final
WORKDIR /app
# Kopiere alles aus dem publish Ordner (inkl. wwwroot, falls vorhanden)
COPY --from=build /app/publish .

# Stelle sicher, dass der wwwroot Ordner existiert
COPY --from=build /src/wwwroot ./wwwroot

ENTRYPOINT ["dotnet", "Sanoa.Api.dll"]
