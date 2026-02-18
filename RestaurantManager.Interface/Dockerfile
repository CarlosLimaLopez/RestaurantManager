# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de proyecto y restaurar dependencias
COPY ["RestaurantManager.Interface/RestaurantManager.Interface.csproj", "RestaurantManager.Interface/"]
COPY ["RestaurantManager.Shared/RestaurantManager.Shared.csproj", "RestaurantManager.Shared/"]
RUN dotnet restore "RestaurantManager.Interface/RestaurantManager.Interface.csproj"

# Copiar el resto de los archivos y publicar
COPY . .
WORKDIR "/src/RestaurantManager.Interface"
RUN dotnet publish "RestaurantManager.Interface.csproj" -c Release -o /app/publish

# Etapa 2: Runtime con Nginx
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

# Copiar los archivos publicados de Blazor WebAssembly
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

# Modificar la base href para que funcione con /admin/
RUN sed -i 's|<base href="/" />|<base href="/admin/" />|g' /usr/share/nginx/html/index.html

# Copiar configuración de Nginx
COPY RestaurantManager.Interface/nginx.conf /etc/nginx/nginx.conf

EXPOSE 8080
