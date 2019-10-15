import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import {
  LOAD_CLANS_FAIL,
  LOAD_CLANS_SUCCESS,
  LOAD_CLAN_SUCCESS,
  LOAD_CLAN_FAIL,
  ADD_CLAN_SUCCESS,
  ADD_CLAN_FAIL,
  DOWNLOAD_CLAN_LOGO_SUCCESS,
  DOWNLOAD_CLAN_LOGO_FAIL,
  UPLOAD_CLAN_LOGO_FAIL,
  UPLOAD_CLAN_LOGO_SUCCESS,
  ClansActionTypes
} from "./types";

export const initialState = [];

export const reducer = (state = initialState, action: ClansActionTypes) => {
  switch (action.type) {
    case LOAD_CLANS_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    }
    case LOAD_CLANS_FAIL: {
      return state;
    }
    case LOAD_CLAN_SUCCESS: {
      return [...state, action.payload.data];
    }
    case LOAD_CLAN_FAIL: {
      return state;
    }
    case DOWNLOAD_CLAN_LOGO_SUCCESS: {
      return [...state, action.payload.data];
    }
    case DOWNLOAD_CLAN_LOGO_FAIL: {
      return state;
    }
    case UPLOAD_CLAN_LOGO_SUCCESS: {
      return state;
    }
    case UPLOAD_CLAN_LOGO_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    case ADD_CLAN_SUCCESS: {
      return state;
    }
    case ADD_CLAN_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    default: {
      return state;
    }
  }
};
