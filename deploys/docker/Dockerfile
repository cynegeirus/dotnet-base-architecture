# ----------------------------------------------------------------------------
# Stage 1: Base Runtime Image
# ----------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble-chiseled-extra AS base
WORKDIR /app
EXPOSE 8080

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_EnableDiagnostics=0
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENV COMPlus_EnableDiagnostics=0

# ----------------------------------------------------------------------------
# Stage 2: Build Stage
# ----------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0-noble AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Presentations/Services/WebAPI/WebAPI.csproj", "Presentations/Services/WebAPI/"]
COPY ["Libraries/Business/Business.csproj", "Libraries/Business/"]
COPY ["Libraries/Core/Core.csproj", "Libraries/Core/"]
COPY ["Libraries/DataAccess/DataAccess.csproj", "Libraries/DataAccess/"]
COPY ["Libraries/Entities/Entities.csproj", "Libraries/Entities/"]

RUN dotnet restore "Presentations/Services/WebAPI/WebAPI.csproj" --runtime linux-x64 --verbosity minimal

COPY . .

WORKDIR "/src/Presentations/Services/WebAPI"

RUN dotnet build "WebAPI.csproj" -c $BUILD_CONFIGURATION --no-restore --runtime linux-x64 -o /app/build

# ----------------------------------------------------------------------------
# Stage 3: Publish Stage
# ----------------------------------------------------------------------------
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

RUN dotnet publish "WebAPI.csproj" \
    -c $BUILD_CONFIGURATION \
    --no-restore \
    --runtime linux-x64 \
    --self-contained false \
    -o /app/publish \
    /p:UseAppHost=false \
    /p:PublishReadyToRun=true \
    /p:PublishTrimmed=false \
    /p:DebugType=None \
    /p:DebugSymbols=false

# ----------------------------------------------------------------------------
# Stage 4: Final Production Image
# ----------------------------------------------------------------------------
FROM base AS final

LABEL maintainer="Ak?n B?ÇER <akin.bicer@outlook.com.tr>" \
      org.opencontainers.image.title="Base Architecture API" \
      org.opencontainers.image.description=".NET 10 N-Tier Architecture Template" \
      org.opencontainers.image.vendor="Akın BİÇER" \
      org.opencontainers.image.licenses="MIT" \
      org.opencontainers.image.source="https://github.com/cynegeirus/dotnet-base-architecture"

WORKDIR /app

COPY --from=publish /app/publish .

HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
    CMD curl --fail http://localhost:8080/health || exit 1

USER $APP_UID

ENTRYPOINT ["dotnet", "WebAPI.dll"]
