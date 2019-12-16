import {
  LOAD_STRIPE_CUSTOMER,
  LOAD_STRIPE_CUSTOMER_SUCCESS,
  LOAD_STRIPE_CUSTOMER_FAIL,
  UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD,
  UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_SUCCESS,
  UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_FAIL,
  StripeCustomerActions
} from "store/actions/payment/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";
import { StripeCustomerState } from "./types";

export const initialState: StripeCustomerState = {
  data: null,
  error: null,
  loading: false
};

export const reducer: Reducer<
  StripeCustomerState,
  StripeCustomerActions
> = produce(
  (draft: Draft<StripeCustomerState>, action: StripeCustomerActions) => {
    switch (action.type) {
      case LOAD_STRIPE_CUSTOMER:
        draft.error = null;
        draft.loading = true;
        break;
      case LOAD_STRIPE_CUSTOMER_SUCCESS:
        draft.data = action.payload.data;
        draft.error = null;
        draft.loading = false;
        break;
      case LOAD_STRIPE_CUSTOMER_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
      case UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD:
        draft.error = null;
        draft.loading = true;
        break;
      case UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_SUCCESS:
        draft.data = action.payload.data;
        draft.error = null;
        draft.loading = false;
        break;
      case UPDATE_STRIPE_CUSTOMER_DEFAULT_PAYMENTMETHOD_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
    }
  },
  initialState
);
