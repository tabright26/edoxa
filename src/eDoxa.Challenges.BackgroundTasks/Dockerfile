FROM microsoft/dotnet:2.2.2-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2.2-sdk AS build
WORKDIR /src
COPY src/eDoxa.Challenges.BackgroundTasks/eDoxa.Challenges.BackgroundTasks.csproj eDoxa.Challenges.BackgroundTasks/
RUN dotnet restore src/eDoxa.Challenges.BackgroundTasks/eDoxa.Challenges.BackgroundTasks.csproj
COPY . .
WORKDIR /src/eDoxa.Challenges.BackgroundTasks
RUN dotnet build --no-restore -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "eDoxa.Challenges.BackgroundTasks.dll"]

EXPOSE 80