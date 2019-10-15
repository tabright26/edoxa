import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import {
  LOAD_PERSONAL_INFO,
  LOAD_PERSONAL_INFO_SUCCESS,
  LOAD_PERSONAL_INFO_FAIL,
  CREATE_PERSONAL_INFO_SUCCESS,
  CREATE_PERSONAL_INFO_FAIL,
  UPDATE_PERSONAL_INFO_SUCCESS,
  UPDATE_PERSONAL_INFO_FAIL,
  PersonalInfoActionTypes,
  PersonalInfoState
} from "./types";
import { Reducer } from "redux";

export const initialState: PersonalInfoState = {
  data: {},
  error: null,
  loading: false
};

export const reducer: Reducer<PersonalInfoState, PersonalInfoActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_PERSONAL_INFO: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_PERSONAL_INFO_SUCCESS: {
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
    case LOAD_PERSONAL_INFO_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case CREATE_PERSONAL_INFO_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case CREATE_PERSONAL_INFO_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    case UPDATE_PERSONAL_INFO_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case UPDATE_PERSONAL_INFO_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
