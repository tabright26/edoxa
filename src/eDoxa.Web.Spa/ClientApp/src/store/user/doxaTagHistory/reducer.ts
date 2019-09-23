import { SubmissionError } from "redux-form";
import { AxiosErrorData } from "interfaces/axios";
import { LOAD_DOXATAG_HISTORY_SUCCESS, LOAD_DOXATAG_HISTORY_FAIL, CHANGE_DOXATAG_SUCCESS, CHANGE_DOXATAG_FAIL, DoxatagHistoryActionTypes } from "./types";

export const initialState = [];

export const reducer = (state = initialState, action: DoxatagHistoryActionTypes) => {
  switch (action.type) {
    case LOAD_DOXATAG_HISTORY_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case CHANGE_DOXATAG_FAIL:
      const { isAxiosError, response } = action.error;
      if (isAxiosError) {
        throw new SubmissionError<AxiosErrorData>(response.data.errors);
      }
      break;
    case CHANGE_DOXATAG_SUCCESS:
    case LOAD_DOXATAG_HISTORY_FAIL:
    default: {
      return state;
    }
  }
};
