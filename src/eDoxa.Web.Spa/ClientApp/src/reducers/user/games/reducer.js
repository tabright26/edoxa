import actionTypes from "actions/identity";

export const initialState = [];

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case actionTypes.LOAD_GAMES_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case actionTypes.LOAD_GAMES_FAIL:
    default: {
      return state;
    }
  }
};
