import { Reducer } from "redux";
import { LOAD_PHONENUMBER_SUCCESS, LOAD_PHONENUMBER_FAIL, PhoneNumberActionTypes, PhoneNumberState } from "./types";

export const initialState: PhoneNumberState = {
  phoneNumber: null,
  phoneNumberVerified: false
};

export const reducer: Reducer<PhoneNumberState, PhoneNumberActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_PHONENUMBER_SUCCESS: {
      return action.payload.data;
    }
    case LOAD_PHONENUMBER_FAIL: {
      return state;
    }
    default: {
      return state;
    }
  }
};
