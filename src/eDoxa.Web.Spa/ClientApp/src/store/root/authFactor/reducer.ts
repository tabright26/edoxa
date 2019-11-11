import { GENERATE_GAME_AUTH_FACTOR, GENERATE_GAME_AUTH_FACTOR_SUCCESS, GENERATE_GAME_AUTH_FACTOR_FAIL, GameAuthFactorActions, GameAuthFactorState } from "./types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";
import { Game, GameOption } from "types";

export const initialState: GameAuthFactorState = {
  data: new Map<Game, GameOption>(),
  error: null,
  loading: false
};

export const reducer: Reducer<GameAuthFactorState, GameAuthFactorActions> = produce((draft: Draft<GameAuthFactorState>, action: GameAuthFactorActions) => {
  switch (action.type) {
    case GENERATE_GAME_AUTH_FACTOR:
      draft.error = null;
      draft.loading = true;
      break;
    case GENERATE_GAME_AUTH_FACTOR_SUCCESS:
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
    case GENERATE_GAME_AUTH_FACTOR_FAIL:
      draft.error = action.error;
      draft.loading = false;
      break;
  }
}, initialState);
