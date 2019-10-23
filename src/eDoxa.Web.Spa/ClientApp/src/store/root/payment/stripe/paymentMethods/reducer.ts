import {
  LOAD_STRIPE_PAYMENTMETHODS,
  LOAD_STRIPE_PAYMENTMETHODS_SUCCESS,
  LOAD_STRIPE_PAYMENTMETHODS_FAIL,
  ATTACH_STRIPE_PAYMENTMETHOD,
  ATTACH_STRIPE_PAYMENTMETHOD_SUCCESS,
  ATTACH_STRIPE_PAYMENTMETHOD_FAIL,
  DETACH_STRIPE_PAYMENTMETHOD,
  DETACH_STRIPE_PAYMENTMETHOD_SUCCESS,
  DETACH_STRIPE_PAYMENTMETHOD_FAIL,
  UPDATE_STRIPE_PAYMENTMETHOD,
  UPDATE_STRIPE_PAYMENTMETHOD_SUCCESS,
  UPDATE_STRIPE_PAYMENTMETHOD_FAIL,
  StripePaymentMethodsActions,
  StripePaymentMethodsState
} from "./types";
import { Reducer } from "redux";

export const initialState: StripePaymentMethodsState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<StripePaymentMethodsState, StripePaymentMethodsActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_STRIPE_PAYMENTMETHODS: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_STRIPE_PAYMENTMETHODS_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204: {
          return { data: state.data, error: null, loading: false };
        }
        default: {
          return { data: data, error: null, loading: false };
        }
      }
    }
    case LOAD_STRIPE_PAYMENTMETHODS_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case ATTACH_STRIPE_PAYMENTMETHOD: {
      return { data: state.data, error: null, loading: true };
    }
    case ATTACH_STRIPE_PAYMENTMETHOD_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case ATTACH_STRIPE_PAYMENTMETHOD_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case DETACH_STRIPE_PAYMENTMETHOD: {
      return { data: state.data, error: null, loading: true };
    }
    case DETACH_STRIPE_PAYMENTMETHOD_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case DETACH_STRIPE_PAYMENTMETHOD_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case UPDATE_STRIPE_PAYMENTMETHOD: {
      return { data: state.data, error: null, loading: true };
    }
    case UPDATE_STRIPE_PAYMENTMETHOD_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case UPDATE_STRIPE_PAYMENTMETHOD_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
