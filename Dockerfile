FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TrafficController.csproj", "./"]
RUN dotnet restore "TrafficController.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "TrafficController.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrafficController.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrafficController.dll"]
