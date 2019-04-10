FROM microsoft/dotnet:2.2.2-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2.2-sdk AS build
WORKDIR /src
COPY src/eDoxa.Identity/eDoxa.Identity.csproj eDoxa.Identity/
RUN dotnet restore src/eDoxa.Identity/eDoxa.Identity.csproj
COPY . .
WORKDIR /src/eDoxa.Identity
RUN dotnet build --no-restore -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "eDoxa.Identity.dll"]

EXPOSE 80
EXPOSE 443