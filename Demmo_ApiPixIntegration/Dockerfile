#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Demmo_ApiPixIntegration/Demmo_ApiPixIntegration.csproj", "Demmo_ApiPixIntegration/"]
RUN dotnet restore "Demmo_ApiPixIntegration/Demmo_ApiPixIntegration.csproj"
COPY . .
WORKDIR "/src/Demmo_ApiPixIntegration"
RUN dotnet build "Demmo_ApiPixIntegration.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demmo_ApiPixIntegration.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demmo_ApiPixIntegration.dll"]