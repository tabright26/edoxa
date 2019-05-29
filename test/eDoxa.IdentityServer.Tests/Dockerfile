FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /app
COPY . .
WORKDIR /app/test/eDoxa.IdentityServer.Tests
ENTRYPOINT ["dotnet", "test", "--logger:trx;LogFileName=/var/tmp/eDoxa.IdentityServer.TestResults.xml"]