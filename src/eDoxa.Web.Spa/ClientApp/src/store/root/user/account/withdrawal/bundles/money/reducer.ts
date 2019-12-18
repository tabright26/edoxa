import {
  LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES,
  LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_SUCCESS,
  LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_FAIL,
  UserAccountDepositBundlesActions
} from "store/actions/cashier/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";
import { UserAccountWithdrawalBundlesState } from "../types";

export const initialState: UserAccountWithdrawalBundlesState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<
  UserAccountWithdrawalBundlesState,
  UserAccountDepositBundlesActions
> = produce(
  (
    draft: Draft<UserAccountWithdrawalBundlesState>,
    action: UserAccountDepositBundlesActions
  ) => {
    switch (action.type) {
      case LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES:
        draft.error = null;
        draft.loading = true;
        break;
      case LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_SUCCESS:
        draft.data = action.payload.data;
        draft.error = null;
        draft.loading = false;
        break;
      case LOAD_USER_ACCOUNT_WITHDRAWAL_MONEY_BUNDLES_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
    }
  },
  initialState
);
