import { loadLeagueOfLegendsSummonerByName } from "./creators";
import actionTypes from "./index";

describe("league of legends actions", () => {
  it("should create an action to get user league of legends id", () => {
    const summonerName = "broman";

    const expectedType = [actionTypes.LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME, actionTypes.LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS, actionTypes.LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL];
    const expectedClient = "leagueOfLegends";
    const expectedMethod = "get";
    const expectedUrl = `/lol/summoner/v4/summoners/by-name/${summonerName}`;

    const actionCreator = loadLeagueOfLegendsSummonerByName(summonerName);

    expect(actionCreator.types).toEqual(expectedType);
    expect(actionCreator.payload.client).toEqual(expectedClient);
    expect(actionCreator.payload.request.method).toEqual(expectedMethod);
    expect(actionCreator.payload.request.url).toEqual(expectedUrl);
  });
});
