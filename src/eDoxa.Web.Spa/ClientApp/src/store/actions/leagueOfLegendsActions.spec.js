import {
  loadLeagueOfLegendsSummonerByName,
  LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME,
  LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS,
  LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL
} from "./leagueOfLegendsActions";

describe("league of legends actions", () => {
  it("should create an action to get user league of legends id", () => {
    const summonerName = "broman";

    const expectedType = [LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME, LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS, LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL];
    const expectedClient = "leagueOfLegends";
    const expectedMethod = "get";
    const expectedUrl = `/lol/summoner/v4/summoners/by-name/${summonerName}`;

    const object = loadLeagueOfLegendsSummonerByName(summonerName);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.client).toEqual(expectedClient);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });
});
