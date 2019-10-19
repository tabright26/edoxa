import {
  LOAD_USER_DOXATAGHISTORY,
  LOAD_USER_DOXATAGHISTORY_SUCCESS,
  LOAD_USER_DOXATAGHISTORY_FAIL,
  UPDATE_USER_DOXATAG,
  UPDATE_USER_DOXATAG_SUCCESS,
  UPDATE_USER_DOXATAG_FAIL,
  UserDoxatagHistoryActions,
  UserDoxatagHistoryState
} from "./types";
import { Reducer } from "redux";

export const initialState: UserDoxatagHistoryState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<UserDoxatagHistoryState, UserDoxatagHistoryActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_USER_DOXATAGHISTORY: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_USER_DOXATAGHISTORY_SUCCESS: {
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
    case LOAD_USER_DOXATAGHISTORY_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case UPDATE_USER_DOXATAG: {
      return { data: state.data, error: null, loading: true };
    }
    case UPDATE_USER_DOXATAG_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case UPDATE_USER_DOXATAG_FAIL: {
      //throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
