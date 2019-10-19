import { Reducer } from "redux";
import { LOAD_USER_PHONE, LOAD_USER_PHONE_SUCCESS, LOAD_USER_PHONE_FAIL, UPDATE_USER_PHONE, UPDATE_USER_PHONE_SUCCESS, UPDATE_USER_PHONE_FAIL, UserPhoneActions, UserPhoneState } from "./types";

export const initialState: UserPhoneState = {
  data: {
    number: null,
    verified: false
  },
  error: null,
  loading: false
};

export const reducer: Reducer<UserPhoneState, UserPhoneActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_USER_PHONE: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_USER_PHONE_SUCCESS: {
      return { data: action.payload.data, error: null, loading: false };
    }
    case LOAD_USER_PHONE_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case UPDATE_USER_PHONE: {
      return { data: state.data, error: null, loading: true };
    }
    case UPDATE_USER_PHONE_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case UPDATE_USER_PHONE_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
