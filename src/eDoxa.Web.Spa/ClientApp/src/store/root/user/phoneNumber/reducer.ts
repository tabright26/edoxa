import { Reducer } from "redux";
import { LOAD_PHONENUMBER, LOAD_PHONENUMBER_SUCCESS, LOAD_PHONENUMBER_FAIL, PhoneNumberActionTypes, PhoneNumberState } from "./types";

export const initialState: PhoneNumberState = {
  data: {
    phoneNumber: null,
    phoneNumberVerified: false
  },
  error: null,
  loading: false
};

export const reducer: Reducer<PhoneNumberState, PhoneNumberActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_PHONENUMBER: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_PHONENUMBER_SUCCESS: {
      return { data: action.payload.data, error: null, loading: false };
    }
    case LOAD_PHONENUMBER_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
