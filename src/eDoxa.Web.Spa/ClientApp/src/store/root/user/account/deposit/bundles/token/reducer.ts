import {
  LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES,
  LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_SUCCESS,
  LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_FAIL,
  UserAccountDepositBundlesActions
} from "store/actions/cashier/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";
import { UserAccountDepositBundlesState } from "../types";

export const initialState: UserAccountDepositBundlesState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<
  UserAccountDepositBundlesState,
  UserAccountDepositBundlesActions
> = produce(
  (
    draft: Draft<UserAccountDepositBundlesState>,
    action: UserAccountDepositBundlesActions
  ) => {
    switch (action.type) {
      case LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES:
        draft.error = null;
        draft.loading = true;
        break;
      case LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_SUCCESS:
        draft.data = action.payload.data;
        draft.error = null;
        draft.loading = false;
        break;
      case LOAD_USER_ACCOUNT_DEPOSIT_TOKEN_BUNDLES_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
    }
  },
  initialState
);
