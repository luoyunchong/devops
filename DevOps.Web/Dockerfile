#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

RUN ls -al


COPY ["DevOps.Web/devops.csproj", "devops/"]
RUN dotnet restore "DevOps.Web/DevOps.Web.csproj"
COPY . .
WORKDIR "/src/devops"
RUN dotnet build "DevOps.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevOps.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Devops.Web.dll"]