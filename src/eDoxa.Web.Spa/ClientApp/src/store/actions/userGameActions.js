export const LOAD_USER_GAMES = 'LOAD_USER_GAMES';
export const LOAD_USER_GAMES_SUCCESS = 'LOAD_USER_GAMES_SUCCESS';
export const LOAD_USER_GAMES_FAIL = 'LOAD_USER_GAMES_FAIL';
export function loadUserGames() {
  return function(dispatch, getState) {
    dispatch({
      types: [LOAD_USER_GAMES, LOAD_USER_GAMES_SUCCESS, LOAD_USER_GAMES_FAIL],
      payload: {
        request: {
          method: 'get',
          url: '/identity/api/games',
          headers: {
            authorization: `Bearer ${getState().oidc.user.access_token}`,
            accept: 'application/json'
          }
        }
      }
    });
  };
}
