import {
  LOAD_USER_TRANSACTION_HISTORY,
  LOAD_USER_TRANSACTION_HISTORY_SUCCESS,
  LOAD_USER_TRANSACTION_HISTORY_FAIL,
  UserTransactionHistoryActions
} from "store/actions/cashier/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";
import { UserTransactionHistoryState } from "./types";

export const initialState: UserTransactionHistoryState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<
  UserTransactionHistoryState,
  UserTransactionHistoryActions
> = produce(
  (
    draft: Draft<UserTransactionHistoryState>,
    action: UserTransactionHistoryActions
  ) => {
    switch (action.type) {
      case LOAD_USER_TRANSACTION_HISTORY: {
        draft.error = null;
        draft.loading = true;
        break;
      }
      case LOAD_USER_TRANSACTION_HISTORY_SUCCESS: {
        const { status, data } = action.payload;
        switch (status) {
          case 204: {
            draft.error = null;
            draft.loading = false;
            break;
          }
          default: {
            draft.data = data;
            draft.error = null;
            draft.loading = false;
            break;
          }
        }
        break;
      }
      case LOAD_USER_TRANSACTION_HISTORY_FAIL: {
        draft.error = action.error;
        draft.loading = false;
        break;
      }
    }
  },
  initialState
);
