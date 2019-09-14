import { LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS, LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL } from "../../../../actions/leagueOfLegendsActions";

export const initialState = {};

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS:
      return action.payload.data;
    case LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL:
    default:
      return state;
  }
};
