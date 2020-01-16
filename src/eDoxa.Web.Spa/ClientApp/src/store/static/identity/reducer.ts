import {
  LOAD_IDENTITY_STATIC_OPTIONS,
  LOAD_IDENTITY_STATIC_OPTIONS_SUCCESS,
  LOAD_IDENTITY_STATIC_OPTIONS_FAIL,
  IdentityStaticOptionsActions
} from "store/actions/static/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";
import { IdentityStaticOptionsState } from "./types";

export const initialState: IdentityStaticOptionsState = {
  data: null,
  error: null,
  loading: false
};

export const reducer: Reducer<
  IdentityStaticOptionsState,
  IdentityStaticOptionsActions
> = produce(
  (
    draft: Draft<IdentityStaticOptionsState>,
    action: IdentityStaticOptionsActions
  ) => {
    switch (action.type) {
      case LOAD_IDENTITY_STATIC_OPTIONS:
        draft.error = null;
        draft.loading = true;
        break;
      case LOAD_IDENTITY_STATIC_OPTIONS_SUCCESS:
        draft.data = action.payload.data;
        draft.error = null;
        draft.loading = false;
        break;
      case LOAD_IDENTITY_STATIC_OPTIONS_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
    }
  },
  initialState
);
