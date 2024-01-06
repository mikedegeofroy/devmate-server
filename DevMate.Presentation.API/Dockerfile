FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DevMate.Presentation.API/DevMate.Presentation.API.csproj", "DevMate.Presentation.API/"]
RUN dotnet restore "DevMate.Presentation.API/DevMate.Presentation.API.csproj"
COPY . .
WORKDIR "/src/ParkingApp.Presentation.API"
RUN dotnet build "DevMate.Presentation.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DevMate.Presentation.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevMate.Presentation.API.dll"]
