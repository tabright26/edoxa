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
  UPDATE_USER_ADDRESS_FAIL
} from "store/actions/identity/types";
import { Reducer } from "redux";
import produce, { Draft } from "immer";
import { UserAddressBookState } from "./types";
import { RootActions } from "store/types";

export const initialState: UserAddressBookState = {
  data: [],
  loading: false
};

export const reducer: Reducer<UserAddressBookState, RootActions> = produce(
  (draft: Draft<UserAddressBookState>, action: RootActions) => {
    switch (action.type) {
      case LOAD_USER_ADDRESSBOOK: {
        draft.loading = true;
        break;
      }
      case LOAD_USER_ADDRESSBOOK_SUCCESS: {
        const { status, data } = action.payload;
        switch (status) {
          case 204: {
            draft.loading = false;
            break;
          }
          default: {
            draft.data = data;
            draft.loading = false;
            break;
          }
        }
        break;
      }
      case LOAD_USER_ADDRESSBOOK_FAIL: {
        draft.loading = false;
        break;
      }
      case CREATE_USER_ADDRESS: {
        draft.loading = true;
        break;
      }
      case CREATE_USER_ADDRESS_SUCCESS: {
        draft.loading = false;
        draft.data.push(action.payload.data);
        break;
      }
      case CREATE_USER_ADDRESS_FAIL: {
        draft.loading = false;
        break;
      }
      case UPDATE_USER_ADDRESS: {
        draft.loading = true;
        break;
      }
      case UPDATE_USER_ADDRESS_SUCCESS: {
        const index = draft.data.findIndex(
          address => address.id === action.payload.data.id
        );
        draft.data[index] = action.payload.data;
        draft.loading = false;
        break;
      }
      case UPDATE_USER_ADDRESS_FAIL: {
        draft.loading = false;
        break;
      }
      case DELETE_USER_ADDRESS: {
        draft.loading = true;
        break;
      }
      case DELETE_USER_ADDRESS_SUCCESS: {
        draft.data = draft.data.filter(
          address => address.id !== action.payload.data.id
        );
        draft.loading = false;
        break;
      }
      case DELETE_USER_ADDRESS_FAIL: {
        draft.loading = false;
        break;
      }
    }
  },
  initialState
);
