import {
  LOAD_STRIPE_BANKACCOUNT,
  LOAD_STRIPE_BANKACCOUNT_SUCCESS,
  LOAD_STRIPE_BANKACCOUNT_FAIL,
  UPDATE_STRIPE_BANKACCOUNT,
  UPDATE_STRIPE_BANKACCOUNT_SUCCESS,
  UPDATE_STRIPE_BANKACCOUNT_FAIL,
  StripeBankAccountActions,
  StripeBankAccountState
} from "./types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";

export const initialState: StripeBankAccountState = {
  data: null,
  error: null,
  loading: false
};

export const reducer: Reducer<StripeBankAccountState, StripeBankAccountActions> = produce((draft: Draft<StripeBankAccountState>, action: StripeBankAccountActions) => {
  switch (action.type) {
    case LOAD_STRIPE_BANKACCOUNT:
      draft.error = null;
      draft.loading = true;
      break;
    case LOAD_STRIPE_BANKACCOUNT_SUCCESS:
      draft.data = action.payload.data;
      draft.error = null;
      draft.loading = false;
      break;
    case LOAD_STRIPE_BANKACCOUNT_FAIL:
      draft.error = action.error;
      draft.loading = false;
      break;
    case UPDATE_STRIPE_BANKACCOUNT:
      draft.error = null;
      draft.loading = true;
      break;
    case UPDATE_STRIPE_BANKACCOUNT_SUCCESS:
      draft.data = action.payload.data;
      draft.error = null;
      draft.loading = false;
      break;
    case UPDATE_STRIPE_BANKACCOUNT_FAIL:
      draft.error = action.error;
      draft.loading = false;
      break;
  }
}, initialState);
