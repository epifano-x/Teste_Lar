# Etapa de compilação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos do projeto
COPY . .

# Restaura as dependências e publica o projeto
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Etapa de execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia os arquivos publicados da etapa de compilação
COPY --from=build /app ./

EXPOSE 5109

# Define o ponto de entrada da aplicação
ENTRYPOINT ["dotnet", "Teste_Lar.dll"]
