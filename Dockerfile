FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
# FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /app
EXPOSE 80

# COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]