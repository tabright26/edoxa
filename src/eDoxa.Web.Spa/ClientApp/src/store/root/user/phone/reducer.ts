import { Reducer } from "redux";
import {
  LOAD_USER_PHONE,
  LOAD_USER_PHONE_SUCCESS,
  LOAD_USER_PHONE_FAIL,
  UPDATE_USER_PHONE,
  UPDATE_USER_PHONE_SUCCESS,
  UPDATE_USER_PHONE_FAIL
} from "store/actions/identity/types";
import produce, { Draft } from "immer";
import { UserPhoneState } from "./types";
import { RootActions } from "store/types";

export const initialState: UserPhoneState = {
  data: null,
  loading: true
};

export const reducer: Reducer<UserPhoneState, RootActions> = produce(
  (draft: Draft<UserPhoneState>, action: RootActions) => {
    switch (action.type) {
      case LOAD_USER_PHONE: {
        draft.loading = true;
        break;
      }
      case LOAD_USER_PHONE_SUCCESS: {
        draft.data = action.payload.data;
        draft.loading = false;
        break;
      }
      case LOAD_USER_PHONE_FAIL: {
        draft.loading = false;
        break;
      }
      case UPDATE_USER_PHONE: {
        draft.loading = false;
        break;
      }
      case UPDATE_USER_PHONE_SUCCESS: {
        draft.data = action.payload.data;
        draft.loading = false;
        break;
      }
      case UPDATE_USER_PHONE_FAIL: {
        draft.loading = false;
        break;
      }
    }
  },
  initialState
);
