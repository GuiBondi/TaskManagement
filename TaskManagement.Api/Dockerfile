#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER $APP_UID
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["TaskManagementApi/TaskManagementApi.csproj", "TaskManagementApi/"]
#RUN dotnet restore "TaskManagementApi/TaskManagementApi.csproj"
#COPY . .
#WORKDIR "/src/TaskManagementApi"
#RUN dotnet build "TaskManagementApi.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "TaskManagementApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "TaskManagementApi.dll"]
# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar os arquivos de projeto e solução
COPY ["TaskManagement.Web/TaskManagement.Web.csproj", "TaskManagement.Web/"]
COPY ["TaskManagement.Core/TaskManagement.Core.csproj", "TaskManagement.Core/"]
COPY ["TaskManagement.Tests/TaskManagement.Tests.csproj", "TaskManagement.Tests/"]

# Restaurar as dependências
RUN dotnet restore "TaskManagement.Web/TaskManagement.Web.csproj"

# Copiar todo o código
COPY . .

# Publicar a aplicação
WORKDIR "/src/TaskManagement.Web"
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expor a porta
EXPOSE 80

# Comando de inicialização
ENTRYPOINT ["dotnet", "TaskManagement.Web.dll"]
