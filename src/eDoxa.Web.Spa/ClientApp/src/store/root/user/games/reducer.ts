import {
  LOAD_GAME_CREDENTIAL,
  LOAD_GAME_CREDENTIAL_SUCCESS,
  LOAD_GAME_CREDENTIAL_FAIL,
  LINK_GAME_CREDENTIAL,
  LINK_GAME_CREDENTIAL_SUCCESS,
  LINK_GAME_CREDENTIAL_FAIL,
  UNLINK_GAME_CREDENTIAL,
  UNLINK_GAME_CREDENTIAL_SUCCESS,
  UNLINK_GAME_CREDENTIAL_FAIL,
  GameCredentialsActions,
  GameCredentialsState
} from "./types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";

export const initialState: GameCredentialsState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<GameCredentialsState, GameCredentialsActions> = produce((draft: Draft<GameCredentialsState>, action: GameCredentialsActions) => {
  switch (action.type) {
    case LOAD_GAME_CREDENTIAL:
      draft.error = null;
      draft.loading = true;
      break;
    case LOAD_GAME_CREDENTIAL_SUCCESS:
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
    case LOAD_GAME_CREDENTIAL_FAIL:
      draft.error = action.error;
      draft.loading = false;
      break;
    case LINK_GAME_CREDENTIAL:
      draft.error = null;
      draft.loading = true;
      break;
    case LINK_GAME_CREDENTIAL_SUCCESS:
      draft.data.push(action.payload.data);
      draft.error = null;
      draft.loading = false;
      break;
    case LINK_GAME_CREDENTIAL_FAIL:
      draft.error = action.error;
      draft.loading = false;
      break;
    case UNLINK_GAME_CREDENTIAL:
      draft.error = null;
      draft.loading = true;
      break;
    case UNLINK_GAME_CREDENTIAL_SUCCESS:
      draft.data = draft.data.filter(credential => credential.game !== action.payload.data.game);
      draft.error = null;
      draft.loading = false;
      break;
    case UNLINK_GAME_CREDENTIAL_FAIL:
      draft.error = action.error;
      draft.loading = false;
      break;
  }
}, initialState);
