import {
  LOAD_GAME_PROVIDERS_SUCCESS,
  LOAD_GAME_PROVIDERS_FAILURE
} from '../actions/userActions';

export const reducer = (state = [], action) => {
  switch (action.type) {
    case LOAD_GAME_PROVIDERS_SUCCESS:
      return action.gameProviders;
    case LOAD_GAME_PROVIDERS_FAILURE:
      return state;
    default:
      return state;
  }
};
