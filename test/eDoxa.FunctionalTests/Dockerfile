FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /tmp
COPY . .
WORKDIR /tmp/test/eDoxa.FunctionalTests
RUN dotnet restore