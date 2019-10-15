import { throwAxiosSubmissionError } from "store/middlewares/axios/types";
import {
  LOAD_ADDRESS_BOOK_SUCCESS,
  LOAD_ADDRESS_BOOK_FAIL,
  ADD_ADDRESS_SUCCESS,
  ADD_ADDRESS_FAIL,
  REMOVE_ADDRESS_SUCCESS,
  REMOVE_ADDRESS_FAIL,
  UPDATE_ADDRESS_SUCCESS,
  UPDATE_ADDRESS_FAIL,
  AddressBookActionTypes
} from "./types";

export const initialState = [];

export const reducer = (state = initialState, action: AddressBookActionTypes) => {
  switch (action.type) {
    case LOAD_ADDRESS_BOOK_SUCCESS: {
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    }
    case LOAD_ADDRESS_BOOK_FAIL: {
      return state;
    }
    case ADD_ADDRESS_SUCCESS: {
      return state;
    }
    case ADD_ADDRESS_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    case UPDATE_ADDRESS_SUCCESS: {
      return state;
    }
    case UPDATE_ADDRESS_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    case REMOVE_ADDRESS_SUCCESS: {
      const { data: addressId } = action.payload;
      return state.filter(address => address.id !== addressId);
    }
    case REMOVE_ADDRESS_FAIL: {
      throwAxiosSubmissionError(action.error);
      return state;
    }
    default: {
      return state;
    }
  }
};
