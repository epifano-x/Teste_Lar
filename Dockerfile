# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Stage 2: Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Open port 5109 for the application
EXPOSE 5109

# Run the application
ENTRYPOINT ["dotnet", "MeuProjeto.dll"]  # Substitua MeuProjeto.dll pelo nome correto
