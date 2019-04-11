FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install -y nodejs

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY . .
WORKDIR /src/src/eDoxa.Web.Spa/ClientApp
RUN npm install --save-dev
RUN npm update --dev
WORKDIR /src/src/eDoxa.Web.Spa
RUN dotnet restore
RUN dotnet build --no-restore -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "eDoxa.Web.Spa.dll"]

EXPOSE 80
EXPOSE 433