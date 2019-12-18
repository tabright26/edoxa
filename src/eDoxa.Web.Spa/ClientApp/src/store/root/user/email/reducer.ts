import { Reducer } from "redux";
import {
  LOAD_USER_EMAIL,
  LOAD_USER_EMAIL_SUCCESS,
  LOAD_USER_EMAIL_FAIL,
  UserEmailActions
} from "store/actions/identity/types";
import produce, { Draft } from "immer";
import { UserEmailState } from "./types";

export const initialState: UserEmailState = {
  data: {
    address: null,
    verified: false
  },
  error: null,
  loading: false
};

export const reducer: Reducer<UserEmailState, UserEmailActions> = produce(
  (draft: Draft<UserEmailState>, action: UserEmailActions) => {
    switch (action.type) {
      case LOAD_USER_EMAIL:
        draft.error = null;
        draft.loading = true;
        break;
      case LOAD_USER_EMAIL_SUCCESS:
        draft.data = action.payload.data;
        draft.error = null;
        draft.loading = false;
        break;
      case LOAD_USER_EMAIL_FAIL:
        draft.error = action.error;
        draft.loading = false;
        break;
    }
  },
  initialState
);
