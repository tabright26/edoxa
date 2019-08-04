import { fetchLeagueOfLeagueAccountByNameAsync } from '../../services/arenaGameService';

export const FETCH_LEAGUEOFLEGENDACCOUNTBYNAME_SUCCESS =
  'FETCH_LEAGUEOFLEGENDACCOUNT_SUCCESS';

export function fetchLeagueOfLegendAccountByNameSuccess(leagueOfLegendAccount) {
  return {
    type: FETCH_LEAGUEOFLEGENDACCOUNTBYNAME_SUCCESS,
    leagueOfLegendAccount
  };
}

export function fetchLeagueOfLegendAccountByName(leagueName) {
  return async function(dispatch, getState) {
    try {
      const response = await fetchLeagueOfLeagueAccountByNameAsync(leagueName);
      dispatch(fetchLeagueOfLegendAccountByNameSuccess(response.data));
    } catch (error) {
      console.log(error);
    }
  };
}
