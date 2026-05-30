# ETAPA 1: Construcción (Usamos el SDK de .NET 10)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiamos la solución y los archivos de proyecto (.csproj) primero.
# Hacemos esto antes de copiar todo el código para aprovechar el caché de Docker.
COPY ["CeluMarket.slnx", "./"]
COPY ["CeluMarket.API/CeluMarket.API.csproj", "CeluMarket.API/"]
COPY ["CeluMarket.Application/CeluMarket.Application.csproj", "CeluMarket.Application/"]
COPY ["CeluMarket.Domain/CeluMarket.Domain.csproj", "CeluMarket.Domain/"]
COPY ["CeluMarket.Infrastructure/CeluMarket.Infrastructure.csproj", "CeluMarket.Infrastructure/"]

# Restauramos las dependencias
RUN dotnet restore "CeluMarket.slnx"

# Ahora sí, copiamos el resto de tu código fuente
COPY . .

# Nos movemos a la capa de la API y compilamos el proyecto en modo "Release"
WORKDIR "/src/CeluMarket.API"
RUN dotnet publish "CeluMarket.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ETAPA 2: Producción (Usamos el Runtime de .NET 10, que es muchísimo más ligero)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Render inyecta automáticamente el puerto en la variable de entorno PORT.
# Le decimos a .NET que escuche en todos los puertos 8080 (el estándar nuevo de .NET)
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

# Traemos solo los archivos ya compilados desde la etapa 1
COPY --from=build /app/publish .

# Comando de arranque de tu aplicación
ENTRYPOINT ["dotnet", "CeluMarket.API.dll"]
