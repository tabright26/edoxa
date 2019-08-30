import { SubmissionError } from "redux-form";
import {
  LOAD_ADDRESS_BOOK_SUCCESS,
  LOAD_ADDRESS_BOOK_FAIL,
  ADD_ADDRESS_SUCCESS,
  ADD_ADDRESS_FAIL,
  UPDATE_ADDRESS_SUCCESS,
  UPDATE_ADDRESS_FAIL,
  REMOVE_ADDRESS_SUCCESS,
  REMOVE_ADDRESS_FAIL
} from "../actions/identityActions";

export const reducer = (state = [], action) => {
  switch (action.type) {
    case LOAD_ADDRESS_BOOK_SUCCESS: {
      const { status, data } = action.payload;
      return status !== 204 ? data : state;
    }
    case ADD_ADDRESS_SUCCESS: {
      return state;
    }
    case UPDATE_ADDRESS_SUCCESS: {
      return state;
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
        throw new SubmissionError(response.data.errors);
      }
      break;
    }
    case LOAD_ADDRESS_BOOK_FAIL:
    default: {
      return state;
    }
  }
};
