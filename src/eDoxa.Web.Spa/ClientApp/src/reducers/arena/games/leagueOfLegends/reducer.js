import actions from "../../../../actions/arena/games/leagueOfLegends";

export const initialState = {};

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case actions.LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_SUCCESS:
      return action.payload.data;
    case actions.LOAD_LEAGUEOFLEGENDS_SUMMONERS_BY_NAME_FAIL:
    default:
      return state;
  }
};
