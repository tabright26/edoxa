import { LOAD_GAMES_SUCCESS, LOAD_GAMES_FAIL, GamesActionTypes } from "./types";

export const initialState = [];

export const reducer = (state = initialState, action: GamesActionTypes) => {
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
    case LOAD_GAMES_FAIL: {
      return state;
    }
    default: {
      return state;
    }
  }
};
