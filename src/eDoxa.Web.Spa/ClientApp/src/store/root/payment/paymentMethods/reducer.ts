import {
  LOAD_PAYMENTMETHODS_SUCCESS,
  LOAD_PAYMENTMETHODS_FAIL,
  DETACH_PAYMENTMETHOD_SUCCESS,
  DETACH_PAYMENTMETHOD_FAIL,
  UPDATE_PAYMENTMETHOD_SUCCESS,
  UPDATE_PAYMENTMETHOD_FAIL,
  PaymentMethodsActionTypes
} from "./types";
import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import { Reducer } from "redux";

export const initialState = { data: [] };

export const reducer: Reducer<any, PaymentMethodsActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_PAYMENTMETHODS_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    }
    case LOAD_PAYMENTMETHODS_FAIL: {
      return state;
    }
    case DETACH_PAYMENTMETHOD_SUCCESS: {
      return state;
    }
    case DETACH_PAYMENTMETHOD_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    case UPDATE_PAYMENTMETHOD_SUCCESS: {
      return state;
    }
    case UPDATE_PAYMENTMETHOD_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    default: {
      return state;
    }
  }
};
