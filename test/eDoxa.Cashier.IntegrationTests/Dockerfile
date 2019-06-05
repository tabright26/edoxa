FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /tmp
COPY . .
WORKDIR /tmp/test/eDoxa.Cashier.IntegrationTests
RUN dotnet restore