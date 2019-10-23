import {
  LOAD_STRIPE_CUSTOMER,
  LOAD_STRIPE_CUSTOMER_SUCCESS,
  LOAD_STRIPE_CUSTOMER_FAIL,
  UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD,
  UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_SUCCESS,
  UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_FAIL,
  StripeCustomerState,
  StripeCustomerActions
} from "./types";
import { Reducer } from "redux";

export const initialState: StripeCustomerState = {
  data: null,
  error: null,
  loading: false
};

export const reducer: Reducer<StripeCustomerState, StripeCustomerActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_STRIPE_CUSTOMER: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_STRIPE_CUSTOMER_SUCCESS: {
      return { data: action.payload.data, error: null, loading: false };
    }
    case LOAD_STRIPE_CUSTOMER_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD: {
      return { data: state.data, error: null, loading: true };
    }
    case UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_SUCCESS: {
      return { data: action.payload.data, error: null, loading: false };
    }
    case UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
