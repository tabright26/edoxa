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
  ClanInvitationsActions,
  ClanInvitationsState
} from "./types";
import { Reducer } from "redux";

export const initialState: ClanInvitationsState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<ClanInvitationsState, ClanInvitationsActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_CLAN_INVITATIONS: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_CLAN_INVITATIONS_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204: {
          return { data: state.data, error: null, loading: false };
        }
        default: {
          return { data: data, error: null, loading: false };
        }
      }
    }
    case LOAD_CLAN_INVITATIONS_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case LOAD_CLAN_INVITATION: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_CLAN_INVITATION_SUCCESS: {
      return { data: [...state.data, action.payload.data], error: null, loading: false };
    }
    case LOAD_CLAN_INVITATION_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case SEND_CLAN_INVITATION: {
      return { data: state.data, error: null, loading: true };
    }
    case SEND_CLAN_INVITATION_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case SEND_CLAN_INVITATION_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case ACCEPT_CLAN_INVITATION: {
      return { data: state.data, error: null, loading: true };
    }
    case ACCEPT_CLAN_INVITATION_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case ACCEPT_CLAN_INVITATION_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case DECLINE_CLAN_INVITATION: {
      return { data: state.data, error: null, loading: true };
    }
    case DECLINE_CLAN_INVITATION_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case DECLINE_CLAN_INVITATION_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
