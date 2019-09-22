import { LoadPaymentMethodsActionType } from "actions/stripe/actionTypes";
import { IAxiosAction } from "interfaces/axios";

export const initialState = { data: [] };

export const reducer = (state = initialState, action: IAxiosAction<LoadPaymentMethodsActionType>) => {
  switch (action.type) {
    case "LOAD_PAYMENTMETHODS_SUCCESS":
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case "LOAD_PAYMENTMETHODS_FAIL":
    default:
      return state;
  }
};
