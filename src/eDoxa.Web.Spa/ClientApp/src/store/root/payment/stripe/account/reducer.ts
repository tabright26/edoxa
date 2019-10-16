import { LOAD_STRIPE_ACCOUNT, LOAD_STRIPE_ACCOUNT_SUCCESS, LOAD_STRIPE_ACCOUNT_FAIL, StripeAccountActions, StripeAccountState } from "./types";
import { Reducer } from "redux";

export const initialState: StripeAccountState = {
  data: {},
  error: null,
  loading: false
};

export const reducer: Reducer<StripeAccountState, StripeAccountActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_STRIPE_ACCOUNT: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_STRIPE_ACCOUNT_SUCCESS: {
      return { data: action.payload.data, error: null, loading: false };
    }
    case LOAD_STRIPE_ACCOUNT_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
