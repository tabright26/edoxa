import {
  LOAD_PAYMENTMETHODS,
  LOAD_PAYMENTMETHODS_SUCCESS,
  LOAD_PAYMENTMETHODS_FAIL,
  DETACH_PAYMENTMETHOD_SUCCESS,
  DETACH_PAYMENTMETHOD_FAIL,
  UPDATE_PAYMENTMETHOD_SUCCESS,
  UPDATE_PAYMENTMETHOD_FAIL,
  PaymentMethodsActionTypes,
  PaymentMethodsState
} from "./types";
import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import { Reducer } from "redux";

export const initialState: PaymentMethodsState = {
  data: { data: [] },
  error: null,
  loading: false
};

export const reducer: Reducer<PaymentMethodsState, PaymentMethodsActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_PAYMENTMETHODS: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_PAYMENTMETHODS_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return { data: state.data, error: null, loading: false };
        default:
          return { data: data, error: null, loading: false };
      }
    }
    case LOAD_PAYMENTMETHODS_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case DETACH_PAYMENTMETHOD_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case DETACH_PAYMENTMETHOD_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    case UPDATE_PAYMENTMETHOD_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case UPDATE_PAYMENTMETHOD_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
