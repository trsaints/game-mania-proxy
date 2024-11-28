# Use the official .NET SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining files and build the application
COPY . ./
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image as a runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Define build arguments
ARG CLIENT_SECRET
ARG PRODUCTION_DOMAIN

# Set environment variables using build arguments
ENV CLIENT_SECRET=${CLIENT_SECRET}
ENV PRODUCTION_DOMAIN=${PRODUCTION_DOMAIN}

# Expose the port the application runs on
EXPOSE 8080

# Run the application
ENTRYPOINT ["dotnet", "GameManiaProxy.dll"]