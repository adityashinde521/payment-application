# Use the .NET SDK image for building and running EF migrations
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory
WORKDIR /src

# Copy the project files and restore the dependencies
COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]
RUN dotnet restore "./WebAPI/WebAPI.csproj"

# Copy the entire source code
COPY . .

# Build the application
WORKDIR "/src/WebAPI"
RUN dotnet build "./WebAPI.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "./WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the ASP.NET Core runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

# Set working directory and expose the necessary port
WORKDIR /app
EXPOSE 80

# Copy the published application from the previous stage
COPY --from=publish /app/publish .

# Copy the entrypoint script
COPY entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

# Set the entrypoint to run migrations and then start the application
ENTRYPOINT ["/entrypoint.sh"]
