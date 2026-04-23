# Etapa base para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["AgroNet.csproj", "./"]
RUN dotnet restore "AgroNet.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "AgroNet.csproj" -c Release -o /app/build

# Etapa de publicación
FROM build AS publish
RUN dotnet publish "AgroNet.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final (Une la base con el código publicado)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgroNet.dll"]