import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import {
  LOAD_USER_ADDRESSBOOK,
  LOAD_USER_ADDRESSBOOK_SUCCESS,
  LOAD_USER_ADDRESSBOOK_FAIL,
  CREATE_USER_ADDRESS_SUCCESS,
  CREATE_USER_ADDRESS_FAIL,
  DELETE_USER_ADDRESS_SUCCESS,
  DELETE_USER_ADDRESS_FAIL,
  UPDATE_USER_ADDRESS_SUCCESS,
  UPDATE_USER_ADDRESS_FAIL,
  UserAddressBookActions,
  UserAddressBookState
} from "./types";
import { Reducer } from "redux";

export const initialState: UserAddressBookState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<UserAddressBookState, UserAddressBookActions> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_USER_ADDRESSBOOK: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_USER_ADDRESSBOOK_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204: {
          return { data: state.data, error: null, loading: false };
        }
        default: {
          return { data: data, error: null, loading: false };
        }
      }
    }
    case LOAD_USER_ADDRESSBOOK_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case CREATE_USER_ADDRESS_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case CREATE_USER_ADDRESS_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    case UPDATE_USER_ADDRESS_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case UPDATE_USER_ADDRESS_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    case DELETE_USER_ADDRESS_SUCCESS: {
      const { data: addressId } = action.payload;
      return { data: state.data.filter(address => address.id !== addressId), error: null, loading: false };
    }
    case DELETE_USER_ADDRESS_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
