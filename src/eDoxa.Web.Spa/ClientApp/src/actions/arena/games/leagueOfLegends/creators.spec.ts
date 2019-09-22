import { loadLeagueOfLegendsSummonerByName, LoadLeagueOfLegendsSummonerByNameActionType } from "./creators";

describe("league of legends actions", () => {
  it("should create an action to get user league of legends id", () => {
    const summonerName = "broman";

    const expectedType: LoadLeagueOfLegendsSummonerByNameActionType[] = [
      "LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME",
      "LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS",
      "LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL"
    ];
    const expectedClient = "leagueOfLegends";
    const expectedMethod = "GET";
    const expectedUrl = `/lol/summoner/v4/summoners/by-name/${summonerName}`;

    const actionCreator = loadLeagueOfLegendsSummonerByName(summonerName);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.client).toEqual(expectedClient);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
