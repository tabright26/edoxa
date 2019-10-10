import { SubmissionError } from "redux-form";
import { AxiosErrorData } from "store/types";

import { LOAD_LOGO_FAIL, LOAD_LOGO_SUCCESS, UPDATE_LOGO_FAIL, UPDATE_LOGO_SUCCESS, LogoActionTypes } from "./types";

export const initialState = [];

export const reducer = (state = initialState, action: LogoActionTypes) => {
  switch (action.type) {
    case LOAD_LOGO_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }

    case UPDATE_LOGO_FAIL: {
      const { isAxiosError, response } = action.error;
      if (isAxiosError) {
        throw new SubmissionError<AxiosErrorData>(response.data.errors);
      }
      break;
    }

    case UPDATE_LOGO_SUCCESS:
    case LOAD_LOGO_FAIL:
    default: {
      return state;
    }
  }
};
