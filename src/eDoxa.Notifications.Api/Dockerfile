FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/eDoxa.Notifications.Api/eDoxa.Notifications.Api.csproj", "src/eDoxa.Notifications.Api/"]
RUN dotnet restore "src/eDoxa.Notifications.Api/eDoxa.Notifications.Api.csproj"
COPY . .
WORKDIR "/src/src/eDoxa.Notifications.Api"
RUN dotnet build "eDoxa.Notifications.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "eDoxa.Notifications.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "eDoxa.Notifications.Api.dll"]