import { Reducer } from "redux";
import { LOAD_EMAIL, LOAD_EMAIL_SUCCESS, LOAD_EMAIL_FAIL, EmailActionTypes, EmailState } from "./types";

export const initialState: EmailState = {
  data: { email: null, emailVerified: false },
  error: null,
  loading: false
};

export const reducer: Reducer<EmailState, EmailActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_EMAIL: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_EMAIL_SUCCESS: {
      return { data: action.payload.data, error: null, loading: false };
    }
    case LOAD_EMAIL_FAIL: {
      return { data: state.data, error: LOAD_EMAIL_FAIL, loading: false };
    }
    default: {
      return state;
    }
  }
};
