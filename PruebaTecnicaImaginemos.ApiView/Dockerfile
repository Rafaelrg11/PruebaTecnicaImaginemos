FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PruebaTecnicaImaginemos.ApiView/PruebaTecnicaImaginemos.ApiView.scproj", "PruebaTecnicaImaginemos.ApiView/"]
COPY ["PruebaTecnicaImaginemos.Application/PruebaTecnicaImaginemos.Application.csproj", "PruebaTecnicaImaginemos.Application/"]
COPY ["PruebaTecnicaImaginemos.Domain/PruebaTecnicaImaginemos.Domain.csproj", "PruebaTecnicaImaginemos.Domain/"]
COPY ["PruebaTecnicaImaginemos.Infraestructure/PruebaTecnicaImaginemos.Infraestructure.csproj", "PruebaTecnicaImaginemos.Infraestructure/"]
RUN dotnet restore "./PruebaTecnicaImaginemos.ApiView/PruebaTecnicaImaginemos.ApiView.csproj"
COPY . .
WORKDIR "/src/PruebaTecnicaImaginemos"
RUN dotnet build "./PruebaTecnicaImaginemos.ApiView.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PruebaTecnicaImaginemos.ApiView.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app 
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PruebaTecnicaImaginemos.ApiView.dll"]








