import {
  LOAD_PAYMENTMETHODS_SUCCESS,
  LOAD_PAYMENTMETHODS_FAIL,
  DETACH_PAYMENTMETHOD_SUCCESS,
  DETACH_PAYMENTMETHOD_FAIL,
  UPDATE_PAYMENTMETHOD_SUCCESS,
  UPDATE_PAYMENTMETHOD_FAIL,
  PaymentMethodsActionTypes
} from "./types";
import { AxiosErrorData } from "store/middlewares/axios/types";
import { Reducer } from "redux";
import { SubmissionError } from "redux-form";

export const initialState = { data: [] };

export const reducer: Reducer<any, PaymentMethodsActionTypes> = (state = initialState, action) => {
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
