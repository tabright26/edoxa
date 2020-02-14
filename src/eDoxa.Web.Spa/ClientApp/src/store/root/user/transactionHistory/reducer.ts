import {
  LOAD_USER_TRANSACTION_HISTORY,
  LOAD_USER_TRANSACTION_HISTORY_SUCCESS,
  LOAD_USER_TRANSACTION_HISTORY_FAIL
} from "store/actions/cashier/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";
import { UserTransactionState } from "./types";
import { RootActions } from "store/types";
import { Transaction } from "types";

export const initialState: UserTransactionState = {
  data: [],
  error: null,
  loading: false
};

const compare = (left: Transaction, right: Transaction) =>
  right.timestamp - left.timestamp;

export const reducer: Reducer<UserTransactionState, RootActions> = produce(
  (draft: Draft<UserTransactionState>, action: RootActions) => {
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
            data.forEach(transaction => {
              const index = draft.data.findIndex(x => x.id === transaction.id);
              if (index === -1) {
                draft.data.push(transaction);
              } else {
                draft.data[index] = transaction;
              }
            });
            draft.data.sort(compare);
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
