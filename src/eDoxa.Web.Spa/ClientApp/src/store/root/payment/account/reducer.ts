import { LOAD_ACCOUNT_SUCCESS, LOAD_ACCOUNT_FAIL, AccountActionTypes } from "./types";
import { Reducer } from "redux";

export const initialState = { data: {} };

export const reducer: Reducer<any, AccountActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_ACCOUNT_SUCCESS: {
      return { data: action.payload.data };
    }
    case LOAD_ACCOUNT_FAIL: {
      return state;
    }
    default: {
      return state;
    }
  }
};
