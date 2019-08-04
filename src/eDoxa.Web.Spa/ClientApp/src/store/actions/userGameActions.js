import { loadUserGamesAsync } from '../../services/userGameService';

export const LOAD_USER_GAMES_SUCCESS = 'LOAD_USER_GAMES_SUCCESS';
export const LOAD_USER_GAMES_FAILURE = 'LOAD_USER_GAMES_FAILURE';

export function loadUserGamesSuccess(games) {
  return { type: LOAD_USER_GAMES_SUCCESS, games };
}

export function loadUserGamesFailure(error) {
  return { type: LOAD_USER_GAMES_FAILURE, error };
}

export function loadUserGames() {
  return async function(dispatch, getState) {
    try {
      const response = await loadUserGamesAsync(getState);
      dispatch(loadUserGamesSuccess(response.data));
    } catch (error) {
      dispatch(loadUserGamesFailure(error));
    }
  };
}
