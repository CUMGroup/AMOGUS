#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AMOGUS.Api/AMOGUS.Api.csproj", "AMOGUS.Api/"]
COPY ["AMOGUS.Core/AMOGUS.Core.csproj", "AMOGUS.Core/"]
COPY ["AMOGUS.Infrastructure/AMOGUS.Infrastructure.csproj", "AMOGUS.Infrastructure/"]
COPY ["AMOGUS.Validation/AMOGUS.Validation.csproj", "AMOGUS.Validation/"]
RUN dotnet restore "AMOGUS.Api/AMOGUS.Api.csproj"
COPY . .
WORKDIR "/src/AMOGUS.Api"
RUN dotnet build "AMOGUS.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AMOGUS.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AMOGUS.Api.dll"]