import { LOAD_BANK_ACCOUNT_SUCCESS, LOAD_BANK_ACCOUNT_FAIL, CHANGE_BANK_ACCOUNT_SUCCESS, CHANGE_BANK_ACCOUNT_FAIL, BankAccountActionTypes } from "./types";
import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import { Reducer } from "redux";

export const initialState = { data: {} };

export const reducer: Reducer<any, BankAccountActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_BANK_ACCOUNT_SUCCESS: {
      return { data: action.payload.data };
    }
    case LOAD_BANK_ACCOUNT_FAIL: {
      return { data: null };
    }
    case CHANGE_BANK_ACCOUNT_SUCCESS: {
      return { data: action.payload.data };
    }
    case CHANGE_BANK_ACCOUNT_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    default: {
      return state;
    }
  }
};
