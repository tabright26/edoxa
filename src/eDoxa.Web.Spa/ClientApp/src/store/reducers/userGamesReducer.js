import { LOAD_GAMES_SUCCESS, LOAD_GAMES_FAIL } from "../actions/identityActions";

export const reducer = (state = [], action) => {
  switch (action.type) {
    case LOAD_GAMES_SUCCESS: {
      const { status, data } = action.payload;
      return status !== 204 ? data : state;
    }
    case LOAD_GAMES_FAIL:
    default: {
      return state;
    }
  }
};
