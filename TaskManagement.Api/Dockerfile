# Etapa 1: Base de construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /src

# Copiar arquivos de projeto
COPY TaskManagement.Api/TaskManagement.Api.csproj TaskManagement.Api/
COPY TaskManagement.Core/TaskManagement.Core.csproj TaskManagement.Core/

# Restaurar dependências
WORKDIR /src/TaskManagement.Api
RUN dotnet restore

# Copiar todo o código-fonte e construir
WORKDIR /src
COPY . .
WORKDIR /src/TaskManagement.Api
RUN dotnet build -c Release -o /app/build

# Etapa 2: Publicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
WORKDIR /src
COPY --from=build-env /src .
RUN dotnet publish TaskManagement.Api/TaskManagement.Api.csproj -c Release -o /app/publish --no-restore

# Etapa 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "TaskManagement.Api.dll"]
