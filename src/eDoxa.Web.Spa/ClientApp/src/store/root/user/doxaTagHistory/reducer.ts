import { LOAD_DOXATAG_HISTORY, LOAD_DOXATAG_HISTORY_SUCCESS, LOAD_DOXATAG_HISTORY_FAIL, CHANGE_DOXATAG_SUCCESS, CHANGE_DOXATAG_FAIL, DoxatagHistoryActionTypes, DoxatagHistoryState } from "./types";
import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import { Reducer } from "redux";

export const initialState: DoxatagHistoryState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<DoxatagHistoryState, DoxatagHistoryActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_DOXATAG_HISTORY: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_DOXATAG_HISTORY_SUCCESS: {
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
    case LOAD_DOXATAG_HISTORY_FAIL: {
      return { data: state.data, error: LOAD_DOXATAG_HISTORY_FAIL, loading: false };
    }
    case CHANGE_DOXATAG_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case CHANGE_DOXATAG_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
