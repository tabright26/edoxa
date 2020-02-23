import { Reducer } from "redux";
import {
  LOAD_CLAN_MEMBERS,
  LOAD_CLAN_MEMBERS_FAIL,
  LOAD_CLAN_MEMBERS_SUCCESS,
  KICK_CLAN_MEMBER,
  KICK_CLAN_MEMBER_FAIL,
  KICK_CLAN_MEMBER_SUCCESS
} from "store/actions/clan/types";
import produce, { Draft } from "immer";
import { ClanMembersState } from "./types";
import { RootActions } from "store/types";

export const initialState: ClanMembersState = {
  data: [],
  loading: false
};

export const reducer: Reducer<ClanMembersState, RootActions> = produce(
  (draft: Draft<ClanMembersState>, action: RootActions) => {
    switch (action.type) {
      case LOAD_CLAN_MEMBERS:
        draft.loading = true;
        break;
      case LOAD_CLAN_MEMBERS_SUCCESS:
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
      case LOAD_CLAN_MEMBERS_FAIL:
        draft.loading = false;
        break;
      case KICK_CLAN_MEMBER:
        draft.loading = true;
        break;
      case KICK_CLAN_MEMBER_SUCCESS:
        draft.loading = false;
        break;
      case KICK_CLAN_MEMBER_FAIL:
        draft.loading = false;
        break;
    }
  },
  initialState
);
