import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import {
  LOAD_CANDIDATURES,
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
  CandidaturesState,
  CandidaturesActionTypes
} from "./types";
import { Reducer } from "redux";

export const initialState: CandidaturesState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<CandidaturesState, CandidaturesActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_CANDIDATURES: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_CANDIDATURES_SUCCESS: {
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
    case LOAD_CANDIDATURES_FAIL: {
      return { data: state.data, error: LOAD_CANDIDATURES_FAIL, loading: false };
    }
    case LOAD_CANDIDATURE_SUCCESS: {
      return { data: [...state.data, action.payload.data], error: null, loading: false };
    }
    case LOAD_CANDIDATURE_FAIL: {
      return { data: state.data, error: LOAD_CANDIDATURE_FAIL, loading: false };
    }
    case ADD_CANDIDATURE_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case ADD_CANDIDATURE_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    case ACCEPT_CANDIDATURE_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case ACCEPT_CANDIDATURE_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    case DECLINE_CANDIDATURE_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case DECLINE_CANDIDATURE_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
