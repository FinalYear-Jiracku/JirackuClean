#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NotificationServices.WebAPI/NotificationServices.WebAPI.csproj", "NotificationServices.WebAPI/"]
COPY ["NotificationServices.Domain/NotificationServices.Domain.csproj", "NotificationServices.Domain/"]
COPY ["NotificationServices.Application/NotificationServices.Application.csproj", "NotificationServices.Application/"]
COPY ["NotificationServices.Persistence/NotificationServices.Persistence.csproj", "NotificationServices.Persistence/"]
COPY ["NotificationServices.Infrastructure/NotificationServices.Infrastructure.csproj", "NotificationServices.Infrastructure/"]
COPY ["NotificationServices.Shared/NotificationServices.Shared.csproj", "NotificationServices.Shared/"]
RUN dotnet restore "NotificationServices.WebAPI/NotificationServices.WebAPI.csproj"
COPY . .
WORKDIR "/src/NotificationServices.WebAPI"
RUN dotnet build "NotificationServices.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationServices.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationServices.WebAPI.dll"]