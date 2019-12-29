import {
  LOAD_USER_PROFILE,
  LOAD_USER_PROFILE_SUCCESS,
  LOAD_USER_PROFILE_FAIL,
  CREATE_USER_PROFILE,
  CREATE_USER_PROFILE_SUCCESS,
  CREATE_USER_PROFILE_FAIL,
  UPDATE_USER_PROFILE,
  UPDATE_USER_PROFILE_SUCCESS,
  UPDATE_USER_PROFILE_FAIL,
  UserProfileActions
} from "store/actions/identity/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";
import { UserProfileState } from "./types";

export const initialState: UserProfileState = {
  data: null,
  error: null,
  loading: false
};

export const reducer: Reducer<
  UserProfileState,
  UserProfileActions
> = produce(
  (draft: Draft<UserProfileState>, action: UserProfileActions) => {
    switch (action.type) {
      case LOAD_USER_PROFILE: {
        draft.error = null;
        draft.loading = true;
        break;
      }
      case LOAD_USER_PROFILE_SUCCESS: {
        draft.data = action.payload.data;
        draft.error = null;
        draft.loading = false;
        break;
      }
      case LOAD_USER_PROFILE_FAIL: {
        draft.error = action.error;
        draft.loading = false;
        break;
      }
      case CREATE_USER_PROFILE: {
        draft.error = null;
        draft.loading = false;
        break;
      }
      case CREATE_USER_PROFILE_SUCCESS: {
        draft.data = action.payload.data;
        draft.error = null;
        draft.loading = false;
        break;
      }
      case CREATE_USER_PROFILE_FAIL: {
        draft.error = action.error;
        draft.loading = false;
        break;
      }
      case UPDATE_USER_PROFILE: {
        draft.error = null;
        draft.loading = false;
        break;
      }
      case UPDATE_USER_PROFILE_SUCCESS: {
        draft.data = action.payload.data;
        draft.error = null;
        draft.loading = false;
        break;
      }
      case UPDATE_USER_PROFILE_FAIL: {
        draft.error = action.error;
        draft.loading = false;
        break;
      }
    }
  },
  initialState
);
