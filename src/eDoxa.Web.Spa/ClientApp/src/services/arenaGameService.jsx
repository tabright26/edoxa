import axios from 'axios';

export async function fetchLeagueOfLeagueAccountByNameAsync(leagueName) {
  console.log(leagueName);
  return await axios({
    method: 'get',
    url:
      'https://na1.api.riotgames.com/lol/summoner/v4/summoners/by-name/' +
      leagueName,
    headers: {
      'X-Riot-Token': process.env.REACT_APP_LEAGUEOFLEGENDS_APIKEY,
      accept: 'application/json'
    }
  });
}
