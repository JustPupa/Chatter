# ===== BUILD STAGE =====
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .

# Dependency Restoration
RUN dotnet restore

# Build and publish
RUN dotnet publish -c Release -o /app/publish

# ===== RUNTIME STAGE =====
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copying the published application
COPY --from=build /app/publish .

# Specifying the port Kestrel listening to
# ENV ASPNETCORE_URLS=http://+:5194
EXPOSE 5194

# Launching the app
ENTRYPOINT ["dotnet", "Cozy_Chatter.dll"]