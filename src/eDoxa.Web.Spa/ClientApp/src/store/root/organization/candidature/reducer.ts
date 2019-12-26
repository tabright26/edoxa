import {
  LOAD_CLAN_CANDIDATURES,
  LOAD_CLAN_CANDIDATURES_SUCCESS,
  LOAD_CLAN_CANDIDATURES_FAIL,
  LOAD_CLAN_CANDIDATURE,
  LOAD_CLAN_CANDIDATURE_SUCCESS,
  LOAD_CLAN_CANDIDATURE_FAIL,
  SEND_CLAN_CANDIDATURE,
  SEND_CLAN_CANDIDATURE_SUCCESS,
  SEND_CLAN_CANDIDATURE_FAIL,
  ACCEPT_CLAN_CANDIDATURE,
  ACCEPT_CLAN_CANDIDATURE_SUCCESS,
  ACCEPT_CLAN_CANDIDATURE_FAIL,
  DECLINE_CLAN_CANDIDATURE,
  DECLINE_CLAN_CANDIDATURE_SUCCESS,
  DECLINE_CLAN_CANDIDATURE_FAIL,
  ClanCandidaturesActions
} from "store/actions/clan/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";
import { ClanCandidaturesState } from "./types";

export const initialState: ClanCandidaturesState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<
  ClanCandidaturesState,
  ClanCandidaturesActions
> = produce(
  (draft: Draft<ClanCandidaturesState>, action: ClanCandidaturesActions) => {
    switch (action.type) {
      case LOAD_CLAN_CANDIDATURES:
        draft.error = null;
        draft.loading = true;
        break;
      case LOAD_CLAN_CANDIDATURES_SUCCESS:
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
      case LOAD_CLAN_CANDIDATURES_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
      case LOAD_CLAN_CANDIDATURE:
        draft.error = null;
        draft.loading = true;
        break;
      case LOAD_CLAN_CANDIDATURE_SUCCESS:
        draft.data = [...draft.data, action.payload.data];
        draft.error = null;
        draft.loading = false;
        break;
      case LOAD_CLAN_CANDIDATURE_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
      case SEND_CLAN_CANDIDATURE:
        draft.error = null;
        draft.loading = true;
        break;
      case SEND_CLAN_CANDIDATURE_SUCCESS:
        draft.error = null;
        draft.loading = false;
        break;
      case SEND_CLAN_CANDIDATURE_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
      case ACCEPT_CLAN_CANDIDATURE:
        draft.error = null;
        draft.loading = true;
        break;
      case ACCEPT_CLAN_CANDIDATURE_SUCCESS:
        draft.error = null;
        draft.loading = false;
        break;
      case ACCEPT_CLAN_CANDIDATURE_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
      case DECLINE_CLAN_CANDIDATURE:
        draft.error = null;
        draft.loading = true;
        break;
      case DECLINE_CLAN_CANDIDATURE_SUCCESS:
        draft.error = null;
        draft.loading = false;
        break;
      case DECLINE_CLAN_CANDIDATURE_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
    }
  },
  initialState
);
