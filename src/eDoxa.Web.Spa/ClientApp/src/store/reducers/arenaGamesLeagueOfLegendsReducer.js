import {
  LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS,
  LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL
} from '../actions/arenaGameActions';

export const reducer = (state = {}, action) => {
  switch (action.type) {
    case LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS:
      return action.payload;
    case LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL:
      console.log(action.payload.error);
      return state;
    default:
      return state;
  }
};
