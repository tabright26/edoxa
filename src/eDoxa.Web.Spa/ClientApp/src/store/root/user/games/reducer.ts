import { LOAD_GAMES, LOAD_GAMES_SUCCESS, LOAD_GAMES_FAIL, GamesActionTypes, GamesState } from "./types";
import { Reducer } from "redux";

export const initialState: GamesState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<GamesState, GamesActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_GAMES: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_GAMES_SUCCESS: {
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
    case LOAD_GAMES_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
