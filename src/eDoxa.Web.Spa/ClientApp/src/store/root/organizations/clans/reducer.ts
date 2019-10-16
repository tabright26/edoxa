import { Reducer } from "redux";
import {
  LOAD_CLANS,
  LOAD_CLANS_FAIL,
  LOAD_CLANS_SUCCESS,
  LOAD_CLAN_SUCCESS,
  LOAD_CLAN_FAIL,
  CREATE_CLAN_SUCCESS,
  CREATE_CLAN_FAIL,
  LEAVE_CLAN_SUCCESS,
  LEAVE_CLAN_FAIL,
  ClansState,
  ClansActions
} from "./types";

export const initialState: ClansState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<ClansState, ClansActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_CLANS: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_CLANS_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return { data: state.data, error: null, loading: false };
        default:
          return { data: data, error: null, loading: false };
      }
    }
    case LOAD_CLANS_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case LOAD_CLAN_SUCCESS: {
      return { data: [...state.data, action.payload.data], error: null, loading: false };
    }
    case LOAD_CLAN_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case LEAVE_CLAN_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case LEAVE_CLAN_FAIL: {
      //throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    case CREATE_CLAN_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case CREATE_CLAN_FAIL: {
      //throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
