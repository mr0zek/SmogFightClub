#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/SFC.Tests/SFC.Tests.csproj", "src/SFC.Tests/"]
COPY ["src/SFC/SFC.csproj", "src/SFC/"]
COPY ["src/SFC.AdminApi/SFC.AdminApi.csproj", "src/SFC.AdminApi/"]
COPY ["src/SFC.Alerts/SFC.Alerts.csproj", "src/SFC.Alerts/"]
COPY ["src/SFC.Infrastructure/SFC.Infrastructure.csproj", "src/SFC.Infrastructure/"]
COPY ["src/SFC.Notifications/SFC.Notifications.csproj", "src/SFC.Notifications/"]
COPY ["src/SFC.Infrastructure.Interfaces/SFC.Infrastructure.Interfaces.csproj", "src/SFC.Infrastructure.Interfaces/"]
COPY ["src/SFC.SharedKernel/SFC.SharedKernel.csproj", "src/SFC.SharedKernel/"]
COPY ["src/SFC.Sensors/SFC.Sensors.csproj", "src/SFC.Sensors/"]
COPY ["src/SFC.Processes/SFC.Processes.csproj", "src/SFC.Processes/"]
COPY ["src/SFC.Accounts/SFC.Accounts.csproj", "src/SFC.Accounts/"]
COPY ["src/SFC.AuthenticationApi/SFC.AuthenticationApi.csproj", "src/SFC.AuthenticationApi/"]
COPY ["src/SFC.SensorApi/SFC.SensorApi.csproj", "src/SFC.SensorApi/"]
COPY ["src/SFC.UsersApi/SFC.UserApi.csproj", "src/SFC.UsersApi/"]
RUN dotnet restore "src/SFC.Tests/SFC.Tests.csproj"
COPY . .
WORKDIR "/src/src/SFC.Tests"
RUN dotnet build "SFC.Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SFC.Tests.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "test", "SFC.Tests.dll"]