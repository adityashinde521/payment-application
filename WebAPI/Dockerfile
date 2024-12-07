# Use the official .NET 6 ASP.NET runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official .NET 6 SDK for the build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Copy the .csproj file first (this allows Docker to cache the restore step)
COPY WebAPI/WebAPI.csproj WebAPI/
# Run dotnet restore to fetch the dependencies
RUN dotnet restore WebAPI/WebAPI.csproj
COPY . .
WORKDIR "/src/WebAPI"
RUN dotnet build "./WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "./WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]
