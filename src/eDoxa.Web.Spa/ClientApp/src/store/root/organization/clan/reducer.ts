import { Reducer } from "redux";
import {
  LOAD_CLANS,
  LOAD_CLANS_FAIL,
  LOAD_CLANS_SUCCESS,
  LOAD_CLAN,
  LOAD_CLAN_SUCCESS,
  LOAD_CLAN_FAIL,
  CREATE_CLAN,
  CREATE_CLAN_SUCCESS,
  CREATE_CLAN_FAIL,
  LEAVE_CLAN,
  LEAVE_CLAN_SUCCESS,
  LEAVE_CLAN_FAIL,
  ClansActions
} from "store/actions/clan/types";
import produce, { Draft } from "immer";
import { ClansState } from "./types";

export const initialState: ClansState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<ClansState, ClansActions> = produce(
  (draft: Draft<ClansState>, action: ClansActions) => {
    switch (action.type) {
      case LOAD_CLANS:
        draft.error = null;
        draft.loading = true;
        break;
      case LOAD_CLANS_SUCCESS:
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
      case LOAD_CLANS_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
      case LOAD_CLAN:
        draft.error = null;
        draft.loading = true;
        break;
      case LOAD_CLAN_SUCCESS:
        draft.data = [...draft.data, action.payload.data];
        draft.error = null;
        draft.loading = false;
        break;
      case LOAD_CLAN_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
      case LEAVE_CLAN:
        draft.error = null;
        draft.loading = true;
        break;
      case LEAVE_CLAN_SUCCESS:
        draft.error = null;
        draft.loading = false;
        break;
      case LEAVE_CLAN_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
      case CREATE_CLAN:
        draft.error = null;
        draft.loading = true;
        break;
      case CREATE_CLAN_SUCCESS:
        draft.error = null;
        draft.loading = false;
        break;
      case CREATE_CLAN_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
    }
  },
  initialState
);
