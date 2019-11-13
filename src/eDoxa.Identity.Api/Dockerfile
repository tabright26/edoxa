FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /tmp

COPY eDoxa.sln .
COPY docker-compose.dcproj .
COPY src/eDoxa.Gateway/eDoxa.Gateway.csproj ./src/eDoxa.Gateway/
COPY src/eDoxa.Cashier.Api/eDoxa.Cashier.Api.csproj ./src/eDoxa.Cashier.Api/
COPY test/eDoxa.Cashier.IntegrationTests/eDoxa.Cashier.IntegrationTests.csproj ./test/eDoxa.Cashier.IntegrationTests/
COPY test/eDoxa.Cashier.UnitTests/eDoxa.Cashier.UnitTests.csproj ./test/eDoxa.Cashier.UnitTests/
COPY src/eDoxa.Challenges.Aggregator/eDoxa.Challenges.Aggregator.csproj ./src/eDoxa.Challenges.Aggregator/
COPY src/eDoxa.Challenges.Api/eDoxa.Challenges.Api.csproj src/eDoxa.Challenges.Api/
COPY src/eDoxa.Challenges.BackgroundTasks/eDoxa.Challenges.BackgroundTasks.csproj ./src/eDoxa.Challenges.BackgroundTasks/
COPY test/eDoxa.Challenges.IntegrationTests/eDoxa.Challenges.IntegrationTests.csproj ./test/eDoxa.Challenges.IntegrationTests/
COPY test/eDoxa.Challenges.UnitTests/eDoxa.Challenges.UnitTests.csproj ./test/eDoxa.Challenges.UnitTests/
COPY src/eDoxa.Clans.Api/eDoxa.Clans.Api.csproj ./src/eDoxa.Clans.Api/
COPY test/eDoxa.Clans.IntegrationTests/eDoxa.Clans.IntegrationTests.csproj ./test/eDoxa.Clans.IntegrationTests/
COPY test/eDoxa.Clans.UnitTests/eDoxa.Clans.UnitTests.csproj ./test/eDoxa.Clans.UnitTests/
COPY src/eDoxa.Games.Api/eDoxa.Games.Api.csproj ./src/eDoxa.Games.Api/
COPY test/eDoxa.Games.IntegrationTests/eDoxa.Games.IntegrationTests.csproj ./test/eDoxa.Games.IntegrationTests/
COPY test/eDoxa.Games.UnitTests/eDoxa.Games.UnitTests.csproj ./test/eDoxa.Games.UnitTests/
COPY src/eDoxa.Identity.Api/eDoxa.Identity.Api.csproj ./src/eDoxa.Identity.Api/
COPY test/eDoxa.Identity.IntegrationTests/eDoxa.Identity.IntegrationTests.csproj ./test/eDoxa.Identity.IntegrationTests/
COPY test/eDoxa.Identity.UnitTests/eDoxa.Identity.UnitTests.csproj ./test/eDoxa.Identity.UnitTests/
COPY src/eDoxa.Notifications.Api/eDoxa.Notifications.Api.csproj ./src/eDoxa.Notifications.Api/
COPY test/eDoxa.Notifications.IntegrationTests/eDoxa.Notifications.IntegrationTests.csproj ./test/eDoxa.Notifications.IntegrationTests/
COPY test/eDoxa.Notifications.UnitTests/eDoxa.Notifications.UnitTests.csproj ./test/eDoxa.Notifications.UnitTests/
COPY src/eDoxa.Payment.Api/eDoxa.Payment.Api.csproj ./src/eDoxa.Payment.Api/
COPY test/eDoxa.Payment.IntegrationTests/eDoxa.Payment.IntegrationTests.csproj ./test/eDoxa.Payment.IntegrationTests/
COPY test/eDoxa.Payment.UnitTests/eDoxa.Payment.UnitTests.csproj ./test/eDoxa.Payment.UnitTests/
COPY src/eDoxa.Web.Spa/eDoxa.Web.Spa.csproj ./src/eDoxa.Web.Spa/
COPY src/eDoxa.Web.Status/eDoxa.Web.Status.csproj ./src/eDoxa.Web.Status/
COPY test/eDoxa.Seedwork.UnitTests/eDoxa.Seedwork.UnitTests.csproj ./test/eDoxa.Seedwork.UnitTests/
COPY test/eDoxa.FunctionalTests/eDoxa.FunctionalTests.csproj ./test/eDoxa.FunctionalTests/

RUN dotnet restore eDoxa.sln

COPY . .
WORKDIR /tmp/src/eDoxa.Identity.Api
RUN dotnet publish -c Release -o /app

FROM build AS unit-tests
WORKDIR /tmp/test/eDoxa.Identity.UnitTests

FROM build AS integration-tests
WORKDIR /tmp/test/eDoxa.Identity.IntegrationTests

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "eDoxa.Identity.Api.dll"]

EXPOSE 80