export const LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME =
  'LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME';
export const LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS =
  'LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS';
export const LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL =
  'LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL';
export function loadLeagueOfLegendsSummonerByName(summonerName) {
  return function(dispatch) {
    dispatch({
      types: [
        LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME,
        LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS,
        LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL
      ],
      payload: {
        client: 'leagueOfLegends',
        request: {
          method: 'get',
          url: `/lol/summoner/v4/summoners/by-name/${summonerName}`,
          headers: {
            'X-Riot-Token': process.env.REACT_APP_LEAGUEOFLEGENDS_RIOT_TOKEN,
            'Accept-Charset': 'application/x-www-form-urlencoded; charset=UTF-8'
          }
        }
      }
    });
  };
}
