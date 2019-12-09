FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /tmp
COPY . .
WORKDIR /tmp/src/eDoxa.Games.Api
RUN dotnet publish -c Release -o /app

FROM build AS unit-tests
WORKDIR /tmp/test/eDoxa.Games.UnitTests

FROM build AS integration-tests
WORKDIR /tmp/test/eDoxa.Games.IntegrationTests

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "eDoxa.Games.Api.dll"]

EXPOSE 80	# HTTP
EXPOSE 81	# gRPC