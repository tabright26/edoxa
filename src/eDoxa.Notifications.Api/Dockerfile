FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /tmp
COPY . .
WORKDIR /tmp/src/eDoxa.Notifications.Api
RUN dotnet publish -c Release -o /app

FROM build AS unit-tests
WORKDIR /tmp/test/eDoxa.Notifications.UnitTests

FROM build AS integration-tests
WORKDIR /tmp/test/eDoxa.Notifications.IntegrationTests

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "eDoxa.Notifications.Api.dll"]

EXPOSE 80	# HTTP
EXPOSE 81	# gRPC