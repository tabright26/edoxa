import { LOAD_GAMES_SUCCESS, LOAD_GAMES_FAIL } from "../actions/identityActions";

export const reducer = (state = [], action) => {
  switch (action.type) {
    case LOAD_GAMES_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    }
    case LOAD_GAMES_FAIL:
    default: {
      return state;
    }
  }
};
