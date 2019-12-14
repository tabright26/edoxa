import {
  LOAD_CLAN_INVITATIONS,
  LOAD_CLAN_INVITATIONS_FAIL,
  LOAD_CLAN_INVITATIONS_SUCCESS,
  LOAD_CLAN_INVITATION,
  LOAD_CLAN_INVITATION_SUCCESS,
  LOAD_CLAN_INVITATION_FAIL,
  SEND_CLAN_INVITATION,
  SEND_CLAN_INVITATION_SUCCESS,
  SEND_CLAN_INVITATION_FAIL,
  ACCEPT_CLAN_INVITATION,
  ACCEPT_CLAN_INVITATION_SUCCESS,
  ACCEPT_CLAN_INVITATION_FAIL,
  DECLINE_CLAN_INVITATION,
  DECLINE_CLAN_INVITATION_SUCCESS,
  DECLINE_CLAN_INVITATION_FAIL,
  ClanInvitationsActions
} from "store/actions/clan/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";
import { ClanInvitationsState } from "./types";

export const initialState: ClanInvitationsState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<
  ClanInvitationsState,
  ClanInvitationsActions
> = produce(
  (draft: Draft<ClanInvitationsState>, action: ClanInvitationsActions) => {
    switch (action.type) {
      case LOAD_CLAN_INVITATIONS:
        draft.error = null;
        draft.loading = true;
        break;
      case LOAD_CLAN_INVITATIONS_SUCCESS:
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
      case LOAD_CLAN_INVITATIONS_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
      case LOAD_CLAN_INVITATION:
        draft.error = null;
        draft.loading = true;
        break;
      case LOAD_CLAN_INVITATION_SUCCESS:
        draft.data = [...draft.data, action.payload.data];
        draft.error = null;
        draft.loading = false;
        break;
      case LOAD_CLAN_INVITATION_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
      case SEND_CLAN_INVITATION:
        draft.error = null;
        draft.loading = true;
        break;
      case SEND_CLAN_INVITATION_SUCCESS:
        draft.error = null;
        draft.loading = false;
        break;
      case SEND_CLAN_INVITATION_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
      case ACCEPT_CLAN_INVITATION:
        draft.error = null;
        draft.loading = true;
        break;
      case ACCEPT_CLAN_INVITATION_SUCCESS:
        draft.error = null;
        draft.loading = false;
        break;
      case ACCEPT_CLAN_INVITATION_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
      case DECLINE_CLAN_INVITATION:
        draft.error = null;
        draft.loading = true;
        break;
      case DECLINE_CLAN_INVITATION_SUCCESS:
        draft.error = null;
        draft.loading = false;
        break;
      case DECLINE_CLAN_INVITATION_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
    }
  },
  initialState
);
