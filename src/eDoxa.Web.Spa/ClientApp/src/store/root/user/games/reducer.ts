import { LOAD_USER_GAMES, LOAD_USER_GAMES_SUCCESS, LOAD_USER_GAMES_FAIL, UserGamesActions, UserGamesState } from "./types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";

export const initialState: UserGamesState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<UserGamesState, UserGamesActions> = produce((draft: Draft<UserGamesState>, action: UserGamesActions) => {
  switch (action.type) {
    case LOAD_USER_GAMES:
      draft.error = null;
      draft.loading = true;
      break;
    case LOAD_USER_GAMES_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          draft.error = null;
          draft.loading = false;
          break;
        default:
          draft.data = data;
          draft.error = null;
          draft.loading = false;
          break;
      }
      break;
    case LOAD_USER_GAMES_FAIL:
      draft.error = action.error;
      draft.loading = false;
      break;
  }
}, initialState);
