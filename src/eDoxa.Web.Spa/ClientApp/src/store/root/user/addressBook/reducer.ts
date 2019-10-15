import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import {
  LOAD_ADDRESS_BOOK,
  LOAD_ADDRESS_BOOK_SUCCESS,
  LOAD_ADDRESS_BOOK_FAIL,
  ADD_ADDRESS_SUCCESS,
  ADD_ADDRESS_FAIL,
  REMOVE_ADDRESS_SUCCESS,
  REMOVE_ADDRESS_FAIL,
  UPDATE_ADDRESS_SUCCESS,
  UPDATE_ADDRESS_FAIL,
  AddressBookActionTypes,
  AddressBookState
} from "./types";
import { Reducer } from "redux";

export const initialState: AddressBookState = {
  data: [],
  error: null,
  loading: false
};

export const reducer: Reducer<AddressBookState, AddressBookActionTypes> = (state = initialState, action) => {
  switch (action.type) {
    case LOAD_ADDRESS_BOOK: {
      return { data: state.data, error: null, loading: true };
    }
    case LOAD_ADDRESS_BOOK_SUCCESS: {
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
    case LOAD_ADDRESS_BOOK_FAIL: {
      return { data: state.data, error: action.error, loading: false };
    }
    case ADD_ADDRESS_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case ADD_ADDRESS_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    case UPDATE_ADDRESS_SUCCESS: {
      return { data: state.data, error: null, loading: false };
    }
    case UPDATE_ADDRESS_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    case REMOVE_ADDRESS_SUCCESS: {
      const { data: addressId } = action.payload;
      return { data: state.data.filter(address => address.id !== addressId), error: null, loading: false };
    }
    case REMOVE_ADDRESS_FAIL: {
      throwAxiosSubmissionError(action.error);
      return { data: state.data, error: action.error, loading: false };
    }
    default: {
      return state;
    }
  }
};
