import axios from 'axios';

export async function loadGameProvidersAsync(getState) {
  const state = getState();
  const { access_token } = state.oidc.user;
  return await axios({
    method: 'get',
    url: `${process.env.REACT_APP_WEB_GATEWAY}/identity/api/game-providers`,
    headers: {
      authorization: `Bearer ${access_token}`,
      accept: 'application/json'
    }
  });
}
