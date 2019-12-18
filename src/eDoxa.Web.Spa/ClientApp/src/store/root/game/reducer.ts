import {
  LOAD_GAMES,
  LOAD_GAMES_SUCCESS,
  LOAD_GAMES_FAIL,
  GamesActions
} from "store/actions/game/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";
import { Game, GameOption } from "types";
import { GamesState } from "./types";

export const initialState: GamesState = {
  data: new Map<Game, GameOption>(),
  error: null,
  loading: false
};

export const reducer: Reducer<GamesState, GamesActions> = produce(
  (draft: Draft<GamesState>, action: GamesActions) => {
    switch (action.type) {
      case LOAD_GAMES:
        draft.error = null;
        draft.loading = true;
        break;
      case LOAD_GAMES_SUCCESS:
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
      case LOAD_GAMES_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
    }
  },
  initialState
);
