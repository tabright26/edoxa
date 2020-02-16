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
  UPDATE_STRIPE_PAYMENTMETHOD_FAIL
} from "store/actions/payment/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";
import { StripePaymentMethodsState } from "./types";
import { RootActions } from "store/types";

export const initialState: StripePaymentMethodsState = {
  data: [],
  loading: false
};

export const reducer: Reducer<StripePaymentMethodsState, RootActions> = produce(
  (draft: Draft<StripePaymentMethodsState>, action: RootActions) => {
    switch (action.type) {
      case LOAD_STRIPE_PAYMENTMETHODS:
        draft.loading = true;
        break;
      case LOAD_STRIPE_PAYMENTMETHODS_SUCCESS:
        const { status, data } = action.payload;
        switch (status) {
          case 204:
            draft.loading = false;
            break;
          default:
            draft.data = data;
            draft.loading = false;
            break;
        }
        break;
      case LOAD_STRIPE_PAYMENTMETHODS_FAIL:
        draft.loading = false;
        break;
      case ATTACH_STRIPE_PAYMENTMETHOD:
        draft.loading = true;
        break;
      case ATTACH_STRIPE_PAYMENTMETHOD_SUCCESS: {
        const { data } = action.payload;
        draft.data.push(data);
        draft.loading = false;
        break;
      }
      case ATTACH_STRIPE_PAYMENTMETHOD_FAIL:
        draft.loading = false;
        break;
      case UPDATE_STRIPE_PAYMENTMETHOD:
        draft.loading = true;
        break;
      case UPDATE_STRIPE_PAYMENTMETHOD_SUCCESS: {
        const { data } = action.payload;
        draft.data[
          draft.data.findIndex(paymentMethod => paymentMethod.id === data.id)
        ] = data;
        draft.loading = false;
        break;
      }
      case UPDATE_STRIPE_PAYMENTMETHOD_FAIL:
        draft.loading = false;
        break;
      case DETACH_STRIPE_PAYMENTMETHOD:
        draft.loading = true;
        break;
      case DETACH_STRIPE_PAYMENTMETHOD_SUCCESS: {
        const { data } = action.payload;
        draft.data = draft.data.filter(
          paymentMethod => paymentMethod.id !== data.id
        );
        draft.loading = false;
        break;
      }
      case DETACH_STRIPE_PAYMENTMETHOD_FAIL:
        draft.loading = false;
        break;
    }
  },
  initialState
);
