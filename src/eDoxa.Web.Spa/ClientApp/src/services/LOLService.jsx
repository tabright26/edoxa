import axios from 'axios';

const apiKey = 'RGAPI-eb72733e-028a-4bb9-ab4d-63fed558924f';

export async function fetchLeagueOfLeagueAccountByNameAsync(leagueName) {
  console.log(leagueName);
  return await axios({
    method: 'get',
    url:
      'https://na1.api.riotgames.com/lol/summoner/v4/summoners/by-name/' +
      leagueName,
    headers: {
      'X-Riot-Token': apiKey,
      accept: 'application/json'
    }
  });
}
