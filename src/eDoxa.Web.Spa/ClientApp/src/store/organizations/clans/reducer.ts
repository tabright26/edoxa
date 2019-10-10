import { SubmissionError } from "redux-form";
import { AxiosErrorData } from "store/types";

import { LOAD_CLANS_FAIL, LOAD_CLANS_SUCCESS, LOAD_CLAN_SUCCESS, LOAD_CLAN_FAIL, ADD_CLAN_SUCCESS, ADD_CLAN_FAIL, ClansActionTypes } from "./types";

export const initialState = [];

export const reducer = (state = initialState, action: ClansActionTypes) => {
  switch (action.type) {
    case LOAD_CLANS_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }

    case LOAD_CLAN_SUCCESS:
      return [...state, action.payload.data];

    case ADD_CLAN_FAIL: {
      const { isAxiosError, response } = action.error;
      if (isAxiosError) {
        throw new SubmissionError<AxiosErrorData>(response.data.errors);
      }
      break;
    }

    case ADD_CLAN_SUCCESS:
    case LOAD_CLANS_FAIL:
    case LOAD_CLAN_FAIL:
    default: {
      return state;
    }
  }
};
