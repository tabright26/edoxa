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
  REFUSE_CLAN_CANDIDATURE,
  REFUSE_CLAN_CANDIDATURE_SUCCESS,
  REFUSE_CLAN_CANDIDATURE_FAIL,
  ClanCandidaturesState,
  ClanCandidaturesActions
} from "./types";
import { Reducer } from "redux";

export const initialState: ClanCandidaturesState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<ClanCandidaturesState, ClanCandidaturesActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_CLAN_CANDIDATURES: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_CLAN_CANDIDATURES_SUCCESS: {
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
    case LOAD_CLAN_CANDIDATURES_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case LOAD_CLAN_CANDIDATURE: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_CLAN_CANDIDATURE_SUCCESS: {
      return { data: [...state.data, action.payload.data], error: null, loading: false };
    }
    case LOAD_CLAN_CANDIDATURE_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case SEND_CLAN_CANDIDATURE: {
      return { data: state.data, error: null, loading: true };
    }
    case SEND_CLAN_CANDIDATURE_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case SEND_CLAN_CANDIDATURE_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case ACCEPT_CLAN_CANDIDATURE: {
      return { data: state.data, error: null, loading: true };
    }
    case ACCEPT_CLAN_CANDIDATURE_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case ACCEPT_CLAN_CANDIDATURE_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case REFUSE_CLAN_CANDIDATURE: {
      return { data: state.data, error: null, loading: true };
    }
    case REFUSE_CLAN_CANDIDATURE_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case REFUSE_CLAN_CANDIDATURE_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
