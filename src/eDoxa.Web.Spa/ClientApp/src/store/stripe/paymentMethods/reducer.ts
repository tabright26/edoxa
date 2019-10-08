import {
  LOAD_PAYMENTMETHODS_SUCCESS,
  LOAD_PAYMENTMETHODS_FAIL,
  DETACH_PAYMENTMETHOD_SUCCESS,
  DETACH_PAYMENTMETHOD_FAIL,
  UPDATE_PAYMENTMETHOD_SUCCESS,
  UPDATE_PAYMENTMETHOD_FAIL,
  PaymentMethodsActionTypes
} from "./types";
import { AxiosErrorData } from "interfaces/axios";
import { SubmissionError } from "redux-form";

export const initialState = { data: [] };

export const reducer = (state = initialState, action: PaymentMethodsActionTypes) => {
  switch (action.type) {
    case LOAD_PAYMENTMETHODS_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case DETACH_PAYMENTMETHOD_FAIL:
    case UPDATE_PAYMENTMETHOD_FAIL:
      const { isAxiosError, response } = action.error;
      if (isAxiosError) {
        throw new SubmissionError<AxiosErrorData>(response.data.errors);
      }
      break;
    case UPDATE_PAYMENTMETHOD_SUCCESS:
    case DETACH_PAYMENTMETHOD_SUCCESS:
    case LOAD_PAYMENTMETHODS_FAIL:
    default:
      return state;
  }
};