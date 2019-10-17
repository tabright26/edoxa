import { Reducer } from "redux";
import { LOAD_CLAN_MEMBERS, LOAD_CLAN_MEMBERS_FAIL, LOAD_CLAN_MEMBERS_SUCCESS, KICK_CLAN_MEMBER, KICK_CLAN_MEMBER_FAIL, KICK_CLAN_MEMBER_SUCCESS, ClanMembersState, ClanMembersActions } from "./types";

export const initialState: ClanMembersState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<ClanMembersState, ClanMembersActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_CLAN_MEMBERS: {
      return {
        data: state.data,
        error: null,
        loading: true
      };
    }
    case LOAD_CLAN_MEMBERS_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204: {
          return {
            data: state.data,
            error: null,
            loading: false
          };
        }
        default: {
          return {
            data: data,
            error: null,
            loading: false
          };
        }
      }
    }
    case LOAD_CLAN_MEMBERS_FAIL: {
      return {
        data: state.data,
        error: action.error,
        loading: false
      };
    }
    case KICK_CLAN_MEMBER: {
      return {
        data: state.data,
        error: null,
        loading: true
      };
    }
    case KICK_CLAN_MEMBER_SUCCESS: {
      return {
        data: state.data,
        error: null,
        loading: false
      };
    }
    case KICK_CLAN_MEMBER_FAIL: {
      return {
        data: state.data,
        error: action.error,
        loading: false
      };
    }
    default: {
      return state;
    }
  }
};
