#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UserServices.WebAPI/UserServices.WebAPI.csproj", "UserServices.WebAPI/"]
COPY ["UserServices.Domain/UserServices.Domain.csproj", "UserServices.Domain/"]
COPY ["UserServices.Application/UserServices.Application.csproj", "UserServices.Application/"]
COPY ["UserServices.Persistence/UserServices.Persistence.csproj", "UserServices.Persistence/"]
COPY ["UserServices.Infrastructure/UserServices.Infrastructure.csproj", "UserServices.Infrastructure/"]
COPY ["UserServices.Shared/UserServices.Shared.csproj", "UserServices.Shared/"]
RUN dotnet restore "UserServices.WebAPI/UserServices.WebAPI.csproj"
COPY . .
WORKDIR "/src/UserServices.WebAPI"
RUN dotnet build "UserServices.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserServices.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserServices.WebAPI.dll"]