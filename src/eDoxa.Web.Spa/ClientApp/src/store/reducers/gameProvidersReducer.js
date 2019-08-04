import {
  LOAD_USER_GAMES_SUCCESS,
  LOAD_USER_GAMES_FAILURE
} from '../actions/userGameActions';

export const reducer = (state = [], action) => {
  switch (action.type) {
    case LOAD_USER_GAMES_SUCCESS:
      return action.games;
    case LOAD_USER_GAMES_FAILURE:
      return state;
    default:
      return state;
  }
};
