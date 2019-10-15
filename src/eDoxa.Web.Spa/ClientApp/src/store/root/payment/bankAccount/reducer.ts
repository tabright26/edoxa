import { LOAD_BANK_ACCOUNT, LOAD_BANK_ACCOUNT_SUCCESS, LOAD_BANK_ACCOUNT_FAIL, CHANGE_BANK_ACCOUNT_SUCCESS, CHANGE_BANK_ACCOUNT_FAIL, BankAccountActionTypes, BankAccountState } from "./types";
import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import { Reducer } from "redux";

export const initialState: BankAccountState = {
  data: {},
  error: null,
  loading: false
};

export const reducer: Reducer<BankAccountState, BankAccountActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_BANK_ACCOUNT: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_BANK_ACCOUNT_SUCCESS: {
      return { data: action.payload.data, error: null, loading: false };
    }
    case LOAD_BANK_ACCOUNT_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case CHANGE_BANK_ACCOUNT_SUCCESS: {
      return { data: action.payload.data, error: null, loading: false };
    }
    case CHANGE_BANK_ACCOUNT_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
