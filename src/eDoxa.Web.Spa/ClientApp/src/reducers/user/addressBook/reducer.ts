import { SubmissionError } from "redux-form";
import { IAxiosAction } from "interfaces/axios";
import { LoadAddressBookActionType, RemoveAddressActionType, AddAddressActionType, UpdateAddressActionType } from "actions/identity/actionTypes";

export const initialState = [];

export const reducer = (state = initialState, action: IAxiosAction<LoadAddressBookActionType | RemoveAddressActionType | AddAddressActionType | UpdateAddressActionType>) => {
  switch (action.type) {
    case "LOAD_ADDRESS_BOOK_SUCCESS":
      const { status, data } = action.payload;
      switch (status) {
        case 204:
          return state;
        default:
          return data;
      }
    case "REMOVE_ADDRESS_SUCCESS": {
      const { data: addressId } = action.payload;
      return state.filter(address => address.id !== addressId);
    }
    case "ADD_ADDRESS_FAIL":
    case "UPDATE_ADDRESS_FAIL":
    case "REMOVE_ADDRESS_FAIL": {
      const { isAxiosError, response } = action.error;
      if (isAxiosError) {
        throw new SubmissionError(response.data.errors);
      }
      return state;
    }
    case "ADD_ADDRESS_SUCCESS":
    case "UPDATE_ADDRESS_SUCCESS":
    case "LOAD_ADDRESS_BOOK_FAIL":
    default: {
      return state;
    }
  }
};
