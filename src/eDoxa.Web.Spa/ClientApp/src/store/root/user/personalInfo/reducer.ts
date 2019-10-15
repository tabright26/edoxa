import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import {
  LOAD_PERSONAL_INFO_SUCCESS,
  LOAD_PERSONAL_INFO_FAIL,
  CREATE_PERSONAL_INFO_SUCCESS,
  CREATE_PERSONAL_INFO_FAIL,
  UPDATE_PERSONAL_INFO_SUCCESS,
  UPDATE_PERSONAL_INFO_FAIL,
  PersonalInfoActionTypes
} from "./types";

export const initialState = null;

export const reducer = (state = initialState, action: PersonalInfoActionTypes) => {
  switch (action.type) {
    case LOAD_PERSONAL_INFO_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    }
    case LOAD_PERSONAL_INFO_FAIL: {
      return state;
    }
    case CREATE_PERSONAL_INFO_SUCCESS: {
      return state;
    }
    case CREATE_PERSONAL_INFO_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    case UPDATE_PERSONAL_INFO_SUCCESS: {
      return state;
    }
    case UPDATE_PERSONAL_INFO_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    default: {
      return state;
    }
  }
};
