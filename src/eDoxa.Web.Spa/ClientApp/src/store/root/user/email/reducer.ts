import { Reducer } from "redux";
import { LOAD_USER_EMAIL, LOAD_USER_EMAIL_SUCCESS, LOAD_USER_EMAIL_FAIL, UserEmailActions, UserEmailState } from "./types";

export const initialState: UserEmailState = {
  data: { email: null, emailVerified: false },
  error: null,
  loading: false
};

export const reducer: Reducer<UserEmailState, UserEmailActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_USER_EMAIL: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_USER_EMAIL_SUCCESS: {
      return { data: action.payload.data, error: null, loading: false };
    }
    case LOAD_USER_EMAIL_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
