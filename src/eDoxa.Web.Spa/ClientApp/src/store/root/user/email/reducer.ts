import { Reducer } from "redux";
import {
  LOAD_USER_EMAIL,
  LOAD_USER_EMAIL_SUCCESS,
  LOAD_USER_EMAIL_FAIL,
  CONFIRM_USER_EMAIL,
  CONFIRM_USER_EMAIL_SUCCESS,
  CONFIRM_USER_EMAIL_FAIL
} from "store/actions/identity/types";
import produce, { Draft } from "immer";
import { UserEmailState } from "./types";
import { RootActions } from "store/types";

export const initialState: UserEmailState = {
  data: null,
  loading: false
};

export const reducer: Reducer<UserEmailState, RootActions> = produce(
  (draft: Draft<UserEmailState>, action: RootActions) => {
    switch (action.type) {
      case LOAD_USER_EMAIL: {
        draft.loading = true;
        break;
      }
      case LOAD_USER_EMAIL_SUCCESS: {
        draft.data = action.payload.data;
        draft.loading = false;
        break;
      }
      case LOAD_USER_EMAIL_FAIL: {
        draft.loading = false;
        break;
      }
      case CONFIRM_USER_EMAIL: {
        draft.loading = false;
        break;
      }
      case CONFIRM_USER_EMAIL_SUCCESS: {
        draft.data = action.payload.data;
        draft.loading = false;
        break;
      }
      case CONFIRM_USER_EMAIL_FAIL: {
        draft.loading = false;
        break;
      }
    }
  },
  initialState
);
