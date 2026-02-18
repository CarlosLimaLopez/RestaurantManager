# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de proyecto y restaurar dependencias
COPY ["RestaurantManager.API/RestaurantManager.API.csproj", "RestaurantManager.API/"]
COPY ["RestaurantManager.Services/RestaurantManager.Services.csproj", "RestaurantManager.Services/"]
COPY ["RestaurantManager.Data/RestaurantManager.Data.csproj", "RestaurantManager.Data/"]
COPY ["RestaurantManager.Model/RestaurantManager.Model.csproj", "RestaurantManager.Model/"]
COPY ["RestaurantManager.Shared/RestaurantManager.Shared.csproj", "RestaurantManager.Shared/"]
RUN dotnet restore "RestaurantManager.API/RestaurantManager.API.csproj"

# Copiar el resto de los archivos y compilar
COPY . .
WORKDIR "/src/RestaurantManager.API"
RUN dotnet build "RestaurantManager.API.csproj" -c Release -o /app/build

# Etapa 2: Publish
FROM build AS publish
RUN dotnet publish "RestaurantManager.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copiar los archivos publicados
COPY --from=publish /app/publish .

# Establecer el punto de entrada
ENTRYPOINT ["dotnet", "RestaurantManager.API.dll"]
