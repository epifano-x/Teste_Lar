# Etapa 1: Imagem de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia csproj e restaura as dependências
COPY *.csproj ./
RUN dotnet restore

# Copia os arquivos do projeto e faz o build
COPY . ./
RUN dotnet build -c Release

# Publica o projeto
RUN dotnet publish -c Release -o out

# Etapa 2: Imagem de Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expõe a porta 5109
EXPOSE 5109

# Define o ponto de entrada da aplicação
ENTRYPOINT ["dotnet", "Teste_Lar.dll"]
