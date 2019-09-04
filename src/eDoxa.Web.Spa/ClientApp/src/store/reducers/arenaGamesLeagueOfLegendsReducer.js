import { LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS, LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL } from "../actions/leagueOfLegendsActions";

export const reducer = (state = {}, action) => {
  switch (action.type) {
    case LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS:
      return action.payload.data;
    case LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL:
      console.log(action.payload);
      return state;
    default:
      return state;
  }
};
