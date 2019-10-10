import { SubmissionError } from "redux-form";
import { AxiosErrorData } from "interfaces/axios";

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
    case LOAD_CANDIDATURES_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }

    case LOAD_CANDIDATURE_SUCCESS:
      return [...state, action.payload.data];

    case ADD_CANDIDATURE_FAIL:
    case ACCEPT_CANDIDATURE_FAIL:
    case DECLINE_CANDIDATURE_FAIL: {
      const { isAxiosError, response } = action.error;
      if (isAxiosError) {
        throw new SubmissionError<AxiosErrorData>(response.data.errors);
      }
      break;
    }

    case ADD_CANDIDATURE_SUCCESS:
    case ACCEPT_CANDIDATURE_SUCCESS:
    case DECLINE_CANDIDATURE_SUCCESS:
    case LOAD_CANDIDATURES_FAIL:
    case LOAD_CANDIDATURE_FAIL:
    default: {
      return state;
    }
  }
};
