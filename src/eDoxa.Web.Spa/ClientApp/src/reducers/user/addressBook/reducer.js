import { SubmissionError } from "redux-form";
import actions from "../../../actions/identity";

export const initialState = [];

export const reducer = (state = initialState, action) => {
  switch (action.type) {
    case actions.LOAD_ADDRESS_BOOK_SUCCESS:
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case actions.REMOVE_ADDRESS_SUCCESS: {
      const { data: addressId } = action.payload;
      return state.filter(address => address.id !== addressId);
    }
    case actions.ADD_ADDRESS_FAIL:
    case actions.UPDATE_ADDRESS_FAIL:
    case actions.REMOVE_ADDRESS_FAIL: {
      const { isAxiosError, response } = action.error;
      if (isAxiosError) {
        throw new SubmissionError(response.data.errors);
      }
      return state;
    }
    case actions.ADD_ADDRESS_SUCCESS:
    case actions.UPDATE_ADDRESS_SUCCESS:
    case actions.LOAD_ADDRESS_BOOK_FAIL:
    default: {
      return state;
    }
  }
};
