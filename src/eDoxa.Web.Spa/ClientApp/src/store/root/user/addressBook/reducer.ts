import {
  LOAD_USER_ADDRESSBOOK,
  LOAD_USER_ADDRESSBOOK_SUCCESS,
  LOAD_USER_ADDRESSBOOK_FAIL,
  CREATE_USER_ADDRESS,
  CREATE_USER_ADDRESS_SUCCESS,
  CREATE_USER_ADDRESS_FAIL,
  DELETE_USER_ADDRESS,
  DELETE_USER_ADDRESS_SUCCESS,
  DELETE_USER_ADDRESS_FAIL,
  UPDATE_USER_ADDRESS,
  UPDATE_USER_ADDRESS_SUCCESS,
  UPDATE_USER_ADDRESS_FAIL,
  UserAddressBookActions
} from "store/actions/identity/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";
import { UserAddressBookState } from "./types";

export const initialState: UserAddressBookState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<
  UserAddressBookState,
  UserAddressBookActions
> = produce(
  (draft: Draft<UserAddressBookState>, action: UserAddressBookActions) => {
    switch (action.type) {
      case LOAD_USER_ADDRESSBOOK: {
        draft.error = null;
        draft.loading = true;
        break;
      }
      case LOAD_USER_ADDRESSBOOK_SUCCESS: {
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
      case LOAD_USER_ADDRESSBOOK_FAIL: {
        draft.error = action.error;
        draft.loading = false;
        break;
      }
      case CREATE_USER_ADDRESS: {
        draft.error = null;
        draft.loading = true;
        break;
      }
      case CREATE_USER_ADDRESS_SUCCESS: {
        draft.error = null;
        draft.loading = false;
        draft.data.push(action.payload.data);
        break;
      }
      case CREATE_USER_ADDRESS_FAIL: {
        draft.error = action.error;
        draft.loading = false;
        break;
      }
      case UPDATE_USER_ADDRESS: {
        draft.error = null;
        draft.loading = true;
        break;
      }
      case UPDATE_USER_ADDRESS_SUCCESS: {
        const index = draft.data.findIndex(
          address => address.id === action.payload.data.id
        );
        draft.data[index] = action.payload.data;
        draft.error = null;
        draft.loading = false;
        break;
      }
      case UPDATE_USER_ADDRESS_FAIL: {
        draft.error = action.error;
        draft.loading = false;
        break;
      }
      case DELETE_USER_ADDRESS: {
        draft.error = null;
        draft.loading = true;
        break;
      }
      case DELETE_USER_ADDRESS_SUCCESS: {
        draft.data = draft.data.filter(
          address => address.id !== action.payload.data.id
        );
        draft.error = null;
        draft.loading = false;
        break;
      }
      case DELETE_USER_ADDRESS_FAIL: {
        draft.error = action.error;
        draft.loading = false;
        break;
      }
    }
  },
  initialState
);
