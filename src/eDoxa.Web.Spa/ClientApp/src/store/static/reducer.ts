import { Reducer } from "redux";
import produce, { Draft } from "immer";
import {
  StaticOptionsActions,
  LOAD_IDENTITY_STATIC_OPTIONS_SUCCESS,
  LOAD_PAYMENT_STATIC_OPTIONS_SUCCESS,
  LOAD_TRANSACTION_BUNDLES_SUCCESS
} from "store/actions/static/types";
import { StaticOptionsState } from "./types";

const initialState: StaticOptionsState = {
  identity: null,
  payment: null,
  transactionBundles: null
};

export const reducer: Reducer<
  StaticOptionsState,
  StaticOptionsActions
> = produce(
  (draft: Draft<StaticOptionsState>, action: StaticOptionsActions) => {
    switch (action.type) {
      case LOAD_IDENTITY_STATIC_OPTIONS_SUCCESS: {
        draft.identity = action.payload.data;
        break;
      }
      case LOAD_PAYMENT_STATIC_OPTIONS_SUCCESS: {
        draft.payment = action.payload.data;
        break;
      }
      case LOAD_TRANSACTION_BUNDLES_SUCCESS: {
        draft.transactionBundles = action.payload.data;
        break;
      }
    }
  },
  initialState
);
