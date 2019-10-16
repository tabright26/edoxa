import { LOAD_STRIPE_BANKACCOUNT, LOAD_STRIPE_BANKACCOUNT_SUCCESS, LOAD_STRIPE_BANKACCOUNT_FAIL, UPDATE_STRIPE_BANKACCOUNT_SUCCESS, UPDATE_STRIPE_BANKACCOUNT_FAIL, StripeBankAccountActions, StripeBankAccountState } from "./types";
import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import { Reducer } from "redux";

export const initialState: StripeBankAccountState = {
  data: {},
  error: null,
  loading: false
};

export const reducer: Reducer<StripeBankAccountState, StripeBankAccountActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_STRIPE_BANKACCOUNT: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_STRIPE_BANKACCOUNT_SUCCESS: {
      return { data: action.payload.data, error: null, loading: false };
    }
    case LOAD_STRIPE_BANKACCOUNT_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case UPDATE_STRIPE_BANKACCOUNT_SUCCESS: {
      return { data: action.payload.data, error: null, loading: false };
    }
    case UPDATE_STRIPE_BANKACCOUNT_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
