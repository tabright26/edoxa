import { Reducer } from "redux";
import { LOAD_EMAIL_SUCCESS, LOAD_EMAIL_FAIL, EmailActionTypes, EmailState } from "./types";

export const initialState = {
  email: "",
  emailVerified: false
};

export const reducer: Reducer<EmailState, EmailActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_EMAIL_SUCCESS:
      return action.payload.data;
    case LOAD_EMAIL_FAIL:
    default: {
      return state;
    }
  }
};
