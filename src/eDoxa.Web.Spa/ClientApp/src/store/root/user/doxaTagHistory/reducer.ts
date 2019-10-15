import { LOAD_DOXATAG_HISTORY_SUCCESS, LOAD_DOXATAG_HISTORY_FAIL, CHANGE_DOXATAG_SUCCESS, CHANGE_DOXATAG_FAIL, DoxatagHistoryActionTypes } from "./types";
import { throwAxiosSubmissionError } from "store/middlewares/axios/types";

export const initialState = [];

export const reducer = (state = initialState, action: DoxatagHistoryActionTypes) => {
  switch (action.type) {
    case LOAD_DOXATAG_HISTORY_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    }
    case LOAD_DOXATAG_HISTORY_FAIL: {
      return state;
    }
    case CHANGE_DOXATAG_SUCCESS: {
      return state;
    }
    case CHANGE_DOXATAG_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    default: {
      return state;
    }
  }
};
