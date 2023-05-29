#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TaskServices.WebAPI/TaskServices.WebAPI.csproj", "TaskServices.WebAPI/"]
RUN dotnet restore "TaskServices.WebAPI/TaskServices.WebAPI.csproj"
COPY . .
WORKDIR "/src/TaskServices.WebAPI"
RUN dotnet build "TaskServices.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskServices.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskServices.WebAPI.dll"]