import { Reducer } from "redux";
import { LOAD_CLAN_MEMBERS, LOAD_CLAN_MEMBERS_FAIL, LOAD_CLAN_MEMBERS_SUCCESS, KICK_CLAN_MEMBER, KICK_CLAN_MEMBER_FAIL, KICK_CLAN_MEMBER_SUCCESS, ClanMembersState, ClanMembersActions } from "./types";
import produce, { Draft } from "immer";

export const initialState: ClanMembersState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<ClanMembersState, ClanMembersActions> = produce((draft: Draft<ClanMembersState>, action: ClanMembersActions) => {
  switch (action.type) {
    case LOAD_CLAN_MEMBERS:
      draft.error = null;
      draft.loading = true;
      break;
    case LOAD_CLAN_MEMBERS_SUCCESS:
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
    case LOAD_CLAN_MEMBERS_FAIL:
      draft.error = action.error;
      draft.loading = false;
      break;
    case KICK_CLAN_MEMBER:
      draft.error = null;
      draft.loading = true;
      break;
    case KICK_CLAN_MEMBER_SUCCESS:
      draft.error = null;
      draft.loading = false;
      break;
    case KICK_CLAN_MEMBER_FAIL:
      draft.error = action.error;
      draft.loading = false;
      break;
  }
}, initialState);
