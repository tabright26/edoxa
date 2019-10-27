FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /tmp
COPY . .
WORKDIR /tmp/src/eDoxa.Arena.Games.Api
RUN dotnet publish -c Release -o /app

FROM build AS unit-tests
WORKDIR /tmp/test/eDoxa.Arena.Games.UnitTests

FROM build AS integration-tests
WORKDIR /tmp/test/eDoxa.Arena.Games.IntegrationTests

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "eDoxa.Arena.Games.Api.dll"]

EXPOSE 80