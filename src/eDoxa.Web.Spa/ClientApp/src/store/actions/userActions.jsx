import { loadGameProvidersAsync } from '../../services/userService';

export const LOAD_GAME_PROVIDERS_SUCCESS = 'LOAD_GAME_PROVIDERS_SUCCESS';
export const LOAD_GAME_PROVIDERS_FAILURE = 'LOAD_GAME_PROVIDERS_FAILURE';

export function loadGameProvidersSuccess(gameProviders) {
  return { type: LOAD_GAME_PROVIDERS_SUCCESS, gameProviders };
}

export function loadGameProvidersFailure(error) {
  return { type: LOAD_GAME_PROVIDERS_FAILURE, error };
}

export function loadGameProviders() {
  return async function(dispatch, getState) {
    try {
      const response = await loadGameProvidersAsync(getState);
      dispatch(loadGameProvidersSuccess(response.data));
    } catch (error) {
      dispatch(loadGameProvidersFailure(error));
    }
  };
}
