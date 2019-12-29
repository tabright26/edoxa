import { Reducer } from "redux";
import {
  LOAD_USER_PHONE,
  LOAD_USER_PHONE_SUCCESS,
  LOAD_USER_PHONE_FAIL,
  UPDATE_USER_PHONE,
  UPDATE_USER_PHONE_SUCCESS,
  UPDATE_USER_PHONE_FAIL,
  UserPhoneActions
} from "store/actions/identity/types";
import produce, { Draft } from "immer";
import { UserPhoneState } from "./types";

export const initialState: UserPhoneState = {
  data: null,
  error: null,
  loading: true
};

export const reducer: Reducer<UserPhoneState, UserPhoneActions> = produce(
  (draft: Draft<UserPhoneState>, action: UserPhoneActions) => {
    switch (action.type) {
      case LOAD_USER_PHONE: {
        draft.error = null;
        draft.loading = true;
        break;
      }
      case LOAD_USER_PHONE_SUCCESS: {
        draft.data = action.payload.data;
        draft.error = null;
        draft.loading = false;
        break;
      }
      case LOAD_USER_PHONE_FAIL: {
        draft.error = action.error;
        draft.loading = false;
        break;
      }
      case UPDATE_USER_PHONE: {
        draft.error = null;
        draft.loading = false;
        break;
      }
      case UPDATE_USER_PHONE_SUCCESS: {
        draft.data = action.payload.data;
        draft.error = null;
        draft.loading = false;
        break;
      }
      case UPDATE_USER_PHONE_FAIL: {
        draft.error = action.error;
        draft.loading = false;
        break;
      }
    }
  },
  initialState
);
