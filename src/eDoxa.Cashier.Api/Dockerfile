FROM microsoft/dotnet:2.2.2-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2.2-sdk AS build
WORKDIR /src
COPY src/eDoxa.Cashier.Api/eDoxa.Cashier.Api.csproj eDoxa.Cashier.Api/
RUN dotnet restore src/eDoxa.Cashier.Api/eDoxa.Cashier.Api.csproj
COPY . .
WORKDIR /src/eDoxa.Cashier.Api
RUN dotnet build --no-restore -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "eDoxa.Cashier.Api.dll"]

EXPOSE 80