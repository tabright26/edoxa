import { LOAD_USER_TOKEN_ACCOUNT_BALANCE, LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS, LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL, UserTokenAccountBalanceActions } from "./types";
import { UserAccountBalanceState } from "../types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";

export const initialState: UserAccountBalanceState = {
  data: {
    available: 0,
    pending: 0
  },
  error: null,
  loading: false
};

export const reducer: Reducer<UserAccountBalanceState, UserTokenAccountBalanceActions> = produce((draft: Draft<UserAccountBalanceState>, action: UserTokenAccountBalanceActions) => {
  switch (action.type) {
    case LOAD_USER_TOKEN_ACCOUNT_BALANCE:
      draft.error = null;
      draft.loading = true;
      break;
    case LOAD_USER_TOKEN_ACCOUNT_BALANCE_SUCCESS:
      draft.data = action.payload.data;
      draft.error = null;
      draft.loading = false;
      break;
    case LOAD_USER_TOKEN_ACCOUNT_BALANCE_FAIL:
      draft.error = action.error;
      draft.loading = false;
      break;
  }
}, initialState);