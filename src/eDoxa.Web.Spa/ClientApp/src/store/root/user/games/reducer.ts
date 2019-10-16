import { LOAD_USER_GAMES, LOAD_USER_GAMES_SUCCESS, LOAD_USER_GAMES_FAIL, UserGamesActions, UserGamesState } from "./types";
import { Reducer } from "redux";

export const initialState: UserGamesState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<UserGamesState, UserGamesActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_USER_GAMES: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_USER_GAMES_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204: {
          return { data: state.data, error: null, loading: false };
        }
        default: {
          return { data: data, error: null, loading: false };
        }
      }
    }
    case LOAD_USER_GAMES_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
