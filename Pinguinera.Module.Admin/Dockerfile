FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Pinguinera.Module.Admin/Pinguinera.Module.Admin.csproj", "Pinguinera.Module.Admin/"]
RUN dotnet restore "Pinguinera.Module.Admin/Pinguinera.Module.Admin.csproj"
COPY . .
WORKDIR "/src/Pinguinera.Module.Admin"
RUN dotnet build "Pinguinera.Module.Admin.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Pinguinera.Module.Admin.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pinguinera.Module.Admin.dll"]
