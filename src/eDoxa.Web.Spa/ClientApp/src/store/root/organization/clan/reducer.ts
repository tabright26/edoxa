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
  LEAVE_CLAN_FAIL
} from "store/actions/clan/types";
import produce, { Draft } from "immer";
import { ClansState } from "./types";
import { RootActions } from "store/types";

export const initialState: ClansState = {
  data: [],
  loading: false
};

export const reducer: Reducer<ClansState, RootActions> = produce(
  (draft: Draft<ClansState>, action: RootActions) => {
    switch (action.type) {
      case LOAD_CLANS:
        draft.loading = true;
        break;
      case LOAD_CLANS_SUCCESS:
        const { status, data } = action.payload;
        switch (status) {
          case 204:
            draft.loading = false;
            break;
          default:
            draft.data = data;
            draft.loading = false;
            break;
        }
        break;
      case LOAD_CLANS_FAIL:
        draft.loading = false;
        break;
      case LOAD_CLAN:
        draft.loading = true;
        break;
      case LOAD_CLAN_SUCCESS:
        draft.data = [...draft.data, action.payload.data];
        draft.loading = false;
        break;
      case LOAD_CLAN_FAIL:
        draft.loading = false;
        break;
      case LEAVE_CLAN:
        draft.loading = true;
        break;
      case LEAVE_CLAN_SUCCESS:
        draft.loading = false;
        break;
      case LEAVE_CLAN_FAIL:
        draft.loading = false;
        break;
      case CREATE_CLAN:
        draft.loading = true;
        break;
      case CREATE_CLAN_SUCCESS:
        draft.loading = false;
        break;
      case CREATE_CLAN_FAIL:
        draft.loading = false;
        break;
    }
  },
  initialState
);
