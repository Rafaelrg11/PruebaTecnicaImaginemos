# Usamos la imagen del SDK de .NET para la fase de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Definimos el directorio de trabajo dentro del contenedor
WORKDIR /src

# Copiamos todos los archivos de la carpeta raíz al contenedor
COPY . /src

# Restauramos las dependencias
RUN dotnet restore PruebaTecnicaImaginemos.sln

# Compilamos la solución
RUN dotnet build PruebaTecnicaImaginemos.sln -c Release -o /app/build

# Publicamos la aplicación
RUN dotnet publish PruebaTecnicaImaginemos.sln -c Release -o /app/publish

# Usamos la imagen del runtime de .NET para la fase de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Definimos el directorio de trabajo en el contenedor
WORKDIR /app

# Copiamos los archivos publicados de la fase de build
COPY --from=build /app/publish .

# Definimos el comando de entrada
ENTRYPOINT ["dotnet", "PruebaTecnicaImaginemos.ApiView.dll"]









