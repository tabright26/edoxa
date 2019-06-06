FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /tmp
COPY . .
WORKDIR /tmp/test/eDoxa.IdentityServer.IntegrationTests
RUN dotnet restore