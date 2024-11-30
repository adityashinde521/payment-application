#!/bin/bash
set -e

echo "Running EF migrations..."
# Run the EF migrations before starting the app
dotnet ef database update

# Start the application
echo "Starting the application..."
exec dotnet WebAPI.dll
