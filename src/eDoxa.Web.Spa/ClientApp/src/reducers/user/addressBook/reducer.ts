import { SubmissionError } from "redux-form";
import { AxiosErrorData } from "interfaces/axios";
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
    case LOAD_ADDRESS_BOOK_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case REMOVE_ADDRESS_SUCCESS: {
      const { data: addressId } = action.payload;
      return state.filter(address => address.id !== addressId);
    }
    case ADD_ADDRESS_FAIL:
    case UPDATE_ADDRESS_FAIL:
    case REMOVE_ADDRESS_FAIL: {
      const { isAxiosError, response } = action.error;
      if (isAxiosError) {
        throw new SubmissionError<AxiosErrorData>(response.data.errors);
      }
      break;
    }
    case ADD_ADDRESS_SUCCESS:
    case UPDATE_ADDRESS_SUCCESS:
    case LOAD_ADDRESS_BOOK_FAIL:
    default: {
      return state;
    }
  }
};
