#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DevOps.Web/DevOps.Web.csproj", "DevOps.Web/"]
COPY ["DevOps.Core/DevOps.Core.csproj", "DevOps.Core/"]
RUN dotnet restore "DevOps.Web/DevOps.Web.csproj"
COPY . .
WORKDIR "/src/DevOps.Web"
RUN dotnet build "DevOps.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevOps.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevOps.Web.dll"]