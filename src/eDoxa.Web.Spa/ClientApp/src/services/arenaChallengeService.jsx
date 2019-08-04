import axios from 'axios';

export async function fetchChallengesAsync(getState) {
  const state = getState();
  const { access_token } = state.oidc.user;
  return await axios({
    method: 'get',
    url: `${process.env.REACT_APP_WEB_GATEWAY}/arena/challenges/api/challenges`,
    headers: {
      authorization: `Bearer ${access_token}`,
      accept: 'application/json'
    }
  });
}

export async function findChallengeAsync(challengeId, getState) {
  const state = getState();
  const { access_token } = state.oidc.user;
  return await axios({
    method: 'get',
    url: `${process.env.REACT_APP_WEB_GATEWAY}/arena/challenges/api/challenges/${challengeId}`,
    headers: {
      authorization: `Bearer ${access_token}`,
      accept: 'application/json'
    }
  });
}
