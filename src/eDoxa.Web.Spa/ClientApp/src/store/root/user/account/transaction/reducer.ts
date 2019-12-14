import {
  LOAD_USER_ACCOUNT_TRANSACTIONS,
  LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS,
  LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL,
  UserAccountTransactionsActions,
  UserAccountTransactionsState
} from "store/actions/account/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";

export const initialState: UserAccountTransactionsState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<
  UserAccountTransactionsState,
  UserAccountTransactionsActions
> = produce(
  (
    draft: Draft<UserAccountTransactionsState>,
    action: UserAccountTransactionsActions
  ) => {
    switch (action.type) {
      case LOAD_USER_ACCOUNT_TRANSACTIONS:
        draft.error = null;
        draft.loading = true;
        break;
      case LOAD_USER_ACCOUNT_TRANSACTIONS_SUCCESS:
        const { status, data } = action.payload;
        switch (status) {
          case 204:
            draft.error = null;
            draft.loading = false;
            break;
          default:
            draft.data = data;
            draft.error = null;
            draft.loading = false;
            break;
        }
        break;
      case LOAD_USER_ACCOUNT_TRANSACTIONS_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
    }
  },
  initialState
);
