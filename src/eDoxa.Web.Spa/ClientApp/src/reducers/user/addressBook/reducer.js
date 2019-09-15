import { SubmissionError } from "redux-form";
import actionTypes from "actions/identity";

export const initialState = [];

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case actionTypes.LOAD_ADDRESS_BOOK_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case actionTypes.REMOVE_ADDRESS_SUCCESS: {
      const { data: addressId } = action.payload;
      return state.filter(address => address.id !== addressId);
    }
    case actionTypes.ADD_ADDRESS_FAIL:
    case actionTypes.UPDATE_ADDRESS_FAIL:
    case actionTypes.REMOVE_ADDRESS_FAIL: {
      const { isAxiosError, response } = action.error;
      if (isAxiosError) {
        throw new SubmissionError(response.data.errors);
      }
      return state;
    }
    case actionTypes.ADD_ADDRESS_SUCCESS:
    case actionTypes.UPDATE_ADDRESS_SUCCESS:
    case actionTypes.LOAD_ADDRESS_BOOK_FAIL:
    default: {
      return state;
    }
  }
};
