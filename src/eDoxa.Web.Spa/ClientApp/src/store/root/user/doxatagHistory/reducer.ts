import {
  LOAD_USER_DOXATAGHISTORY,
  LOAD_USER_DOXATAGHISTORY_SUCCESS,
  LOAD_USER_DOXATAGHISTORY_FAIL,
  CHANGE_USER_DOXATAG,
  CHANGE_USER_DOXATAG_SUCCESS,
  CHANGE_USER_DOXATAG_FAIL,
  UserDoxatagHistoryActions
} from "store/actions/identity/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";
import { UserDoxatagHistoryState } from "./types";

export const initialState: UserDoxatagHistoryState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<
  UserDoxatagHistoryState,
  UserDoxatagHistoryActions
> = produce(
  (
    draft: Draft<UserDoxatagHistoryState>,
    action: UserDoxatagHistoryActions
  ) => {
    switch (action.type) {
      case LOAD_USER_DOXATAGHISTORY: {
        draft.error = null;
        draft.loading = true;
        break;
      }
      case LOAD_USER_DOXATAGHISTORY_SUCCESS: {
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
      case LOAD_USER_DOXATAGHISTORY_FAIL: {
        draft.error = action.error;
        draft.loading = false;
        break;
      }
      case CHANGE_USER_DOXATAG: {
        draft.error = null;
        draft.loading = true;
        break;
      }
      case CHANGE_USER_DOXATAG_SUCCESS: {
        draft.data.push(action.payload.data);
        draft.error = null;
        draft.loading = false;
        break;
      }
      case CHANGE_USER_DOXATAG_FAIL: {
        draft.error = action.error;
        draft.loading = false;
        break;
      }
    }
  },
  initialState
);
