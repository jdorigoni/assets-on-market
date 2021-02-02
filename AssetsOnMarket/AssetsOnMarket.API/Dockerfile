#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AssetsOnMarket.Api/AssetsOnMarket.API.csproj", "AssetsOnMarket.Api/"]
COPY ["AssetsOnMarket.Infrastructure.IoC/AssetsOnMarket.Infrastructure.IoC.csproj", "AssetsOnMarket.Infrastructure.IoC/"]
COPY ["AssetsOnMarket.Infrastructure.Data/AssetsOnMarket.Infrastructure.Data.csproj", "AssetsOnMarket.Infrastructure.Data/"]
COPY ["AssetsOnMarket.Domain/AssetsOnMarket.Domain.csproj", "AssetsOnMarket.Domain/"]
COPY ["AssetsOnMarket.Domain.Core/AssetsOnMarket.Domain.Core.csproj", "AssetsOnMarket.Domain.Core/"]
COPY ["AssetsOnMarket.Infrastructure.Bus/AssetsOnMarket.Infrastructure.Bus.csproj", "AssetsOnMarket.Infrastructure.Bus/"]
COPY ["AssetsOnMarket.Application/AssetsOnMarket.Application.csproj", "AssetsOnMarket.Application/"]
RUN dotnet restore "AssetsOnMarket.Api/AssetsOnMarket.API.csproj"
COPY . .
WORKDIR "/src/AssetsOnMarket.Api"
RUN dotnet build "AssetsOnMarket.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AssetsOnMarket.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AssetsOnMarket.API.dll"]