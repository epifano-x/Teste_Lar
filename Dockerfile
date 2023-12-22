# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /
VOLUME /root/.aspnet/DataProtection-Keys

# Copy csproj and restore as distinct layers
COPY ["Teste_Lar.csproj", "./"]
RUN dotnet restore "Teste_Lar.csproj"

# Copy everything else and build
COPY . ./
RUN dotnet publish "Teste_Lar.csproj" -c Release -o out

# Stage 2: Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Open port 5109 for the application
EXPOSE 5109

# Run the application
ENTRYPOINT ["dotnet", "Teste_Lar.dll"]
