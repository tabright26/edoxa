import { LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME, LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS, LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL, LeagueOfLegendsActionCreators } from "./types";

export function loadLeagueOfLegendsSummonerByName(summonerName: string): LeagueOfLegendsActionCreators {
  return {
    types: [LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME, LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS, LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL],
    payload: {
      client: "leagueOfLegends",
      request: {
        method: "GET",
        url: `/lol/summoner/v4/summoners/by-name/${summonerName}`
      }
    }
  };
}
