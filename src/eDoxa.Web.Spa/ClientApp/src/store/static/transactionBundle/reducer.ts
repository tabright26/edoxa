import {
  LOAD_TRANSACTION_BUNDLES,
  LOAD_TRANSACTION_BUNDLES_SUCCESS,
  LOAD_TRANSACTION_BUNDLES_FAIL,
  TransactionBundlesActions
} from "store/actions/cashier/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";
import { TransactionBundlesState } from "./types";

export const initialState: TransactionBundlesState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<
  TransactionBundlesState,
  TransactionBundlesActions
> = produce(
  (
    draft: Draft<TransactionBundlesState>,
    action: TransactionBundlesActions
  ) => {
    switch (action.type) {
      case LOAD_TRANSACTION_BUNDLES:
        draft.error = null;
        draft.loading = true;
        break;
      case LOAD_TRANSACTION_BUNDLES_SUCCESS:
        draft.data = action.payload.data;
        draft.error = null;
        draft.loading = false;
        break;
      case LOAD_TRANSACTION_BUNDLES_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
    }
  },
  initialState
);
