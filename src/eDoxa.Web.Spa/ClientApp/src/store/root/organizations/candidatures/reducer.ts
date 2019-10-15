import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import {
  LOAD_CANDIDATURES_SUCCESS,
  LOAD_CANDIDATURES_FAIL,
  LOAD_CANDIDATURE_SUCCESS,
  LOAD_CANDIDATURE_FAIL,
  ADD_CANDIDATURE_SUCCESS,
  ADD_CANDIDATURE_FAIL,
  ACCEPT_CANDIDATURE_SUCCESS,
  ACCEPT_CANDIDATURE_FAIL,
  DECLINE_CANDIDATURE_SUCCESS,
  DECLINE_CANDIDATURE_FAIL,
  CandidaturesActionTypes
} from "./types";

export const initialState = [];

export const reducer = (state = initialState, action: CandidaturesActionTypes) => {
  switch (action.type) {
    case LOAD_CANDIDATURES_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    }
    case LOAD_CANDIDATURES_FAIL: {
      return state;
    }
    case LOAD_CANDIDATURE_SUCCESS: {
      return [...state, action.payload.data];
    }
    case LOAD_CANDIDATURE_FAIL: {
      return state;
    }
    case ADD_CANDIDATURE_SUCCESS: {
      return state;
    }
    case ADD_CANDIDATURE_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    case ACCEPT_CANDIDATURE_SUCCESS: {
      return state;
    }
    case ACCEPT_CANDIDATURE_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    case DECLINE_CANDIDATURE_SUCCESS: {
      return state;
    }
    case DECLINE_CANDIDATURE_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    default: {
      return state;
    }
  }
};
