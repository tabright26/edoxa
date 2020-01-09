import {
  LOAD_USER_TRANSACTION_HISTORY,
  LOAD_USER_TRANSACTION_HISTORY_SUCCESS,
  LOAD_USER_TRANSACTION_HISTORY_FAIL,
  UserTransactionActions,
  CREATE_USER_TRANSACTION,
  CREATE_USER_TRANSACTION_SUCCESS,
  CREATE_USER_TRANSACTION_FAIL
} from "store/actions/cashier/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";
import { UserTransactionState } from "./types";

export const initialState: UserTransactionState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<
  UserTransactionState,
  UserTransactionActions
> = produce(
  (
    draft: Draft<UserTransactionState>,
    action: UserTransactionActions
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
      case CREATE_USER_TRANSACTION: {
        draft.error = null;
        draft.loading = false;
        break;
      }
      case CREATE_USER_TRANSACTION_SUCCESS: {
        draft.data.push(action.payload.data);
        draft.error = null;
        draft.loading = false;
        break;
      }
      case CREATE_USER_TRANSACTION_FAIL: {
        draft.error = action.error;
        draft.loading = false;
        break;
      }
    }
  },
  initialState
);
