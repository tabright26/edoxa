FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /app
COPY . .
WORKDIR /app/test/eDoxa.Arena.Challenges.Tests
ENTRYPOINT ["dotnet", "test", "--logger:trx;LogFileName=/var/tmp/eDoxa.Arena.Challenges.TestResults.xml"]