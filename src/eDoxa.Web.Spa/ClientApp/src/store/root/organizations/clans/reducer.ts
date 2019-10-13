import { SubmissionError } from "redux-form";
import { AxiosErrorData } from "store/middlewares/axios/types";

import {
  LOAD_CLANS_FAIL,
  LOAD_CLANS_SUCCESS,
  LOAD_CLAN_SUCCESS,
  LOAD_CLAN_FAIL,
  ADD_CLAN_SUCCESS,
  ADD_CLAN_FAIL,
  LOAD_LOGO_FAIL,
  LOAD_LOGO_SUCCESS,
  UPDATE_LOGO_FAIL,
  UPDATE_LOGO_SUCCESS,
  ClansActionTypes
} from "./types";

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

    case LOAD_LOGO_SUCCESS:
    case LOAD_CLAN_SUCCESS:
      return [...state, action.payload.data];

    case UPDATE_LOGO_FAIL:
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
    case UPDATE_LOGO_SUCCESS:
    case LOAD_LOGO_FAIL:
    default: {
      return state;
    }
  }
};
