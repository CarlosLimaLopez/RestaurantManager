# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de proyecto y restaurar dependencias
COPY ["RestaurantManager.Interface.Public/RestaurantManager.Interface.Public.csproj", "RestaurantManager.Interface.Public/"]
COPY ["RestaurantManager.Shared/RestaurantManager.Shared.csproj", "RestaurantManager.Shared/"]
RUN dotnet restore "RestaurantManager.Interface.Public/RestaurantManager.Interface.Public.csproj"

# Copiar el resto de los archivos y compilar
COPY . .
WORKDIR "/src/RestaurantManager.Interface.Public"
RUN dotnet build "RestaurantManager.Interface.Public.csproj" -c Release -o /app/build

# Etapa 2: Publish
FROM build AS publish
RUN dotnet publish "RestaurantManager.Interface.Public.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Verificar que wwwroot se publicó correctamente
RUN echo "=== Checking wwwroot files ===" && \
    ls -la /app/publish/wwwroot && \
    ls -la /app/publish/wwwroot/app.css && \
    echo "=== app.css found ===" || echo "WARNING: app.css not found"

# Etapa 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080

# Copiar los archivos publicados
COPY --from=publish /app/publish .

# Establecer el punto de entrada
ENTRYPOINT ["dotnet", "RestaurantManager.Interface.Public.dll"]
