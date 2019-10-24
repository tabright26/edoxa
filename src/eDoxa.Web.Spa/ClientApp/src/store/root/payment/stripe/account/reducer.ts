import { LOAD_STRIPE_ACCOUNT, LOAD_STRIPE_ACCOUNT_SUCCESS, LOAD_STRIPE_ACCOUNT_FAIL, StripeAccountActions, StripeAccountState } from "./types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";

export const initialState: StripeAccountState = {
  data: null,
  error: null,
  loading: false
};

export const reducer: Reducer<StripeAccountState, StripeAccountActions> = produce((draft: Draft<StripeAccountState>, action: StripeAccountActions) => {
  switch (action.type) {
    case LOAD_STRIPE_ACCOUNT:
      draft.error = null;
      draft.loading = true;
      break;
    case LOAD_STRIPE_ACCOUNT_SUCCESS:
      draft.data = action.payload.data;
      draft.error = null;
      draft.loading = false;
      break;
    case LOAD_STRIPE_ACCOUNT_FAIL:
      draft.error = action.error;
      draft.loading = false;
      break;
  }
}, initialState);
