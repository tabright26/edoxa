import {
  LOAD_ADDRESS_BOOK,
  LOAD_ADDRESS_BOOK_SUCCESS,
  LOAD_ADDRESS_BOOK_FAIL,
  ADD_ADDRESS,
  ADD_ADDRESS_SUCCESS,
  ADD_ADDRESS_FAIL,
  REMOVE_ADDRESS,
  REMOVE_ADDRESS_SUCCESS,
  REMOVE_ADDRESS_FAIL,
  UPDATE_ADDRESS,
  UPDATE_ADDRESS_SUCCESS,
  UPDATE_ADDRESS_FAIL,
  AddressBookActionCreators
} from "./types";

export function loadAddressBook(): AddressBookActionCreators {
  return {
    types: [LOAD_ADDRESS_BOOK, LOAD_ADDRESS_BOOK_SUCCESS, LOAD_ADDRESS_BOOK_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/identity/api/address-book`
      }
    }
  };
}

export function addAddress(data: any): AddressBookActionCreators {
  return {
    types: [ADD_ADDRESS, ADD_ADDRESS_SUCCESS, ADD_ADDRESS_FAIL],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/address-book",
        data
      }
    }
  };
}

export function updateAddress(addressId: string, data: any): AddressBookActionCreators {
  return {
    types: [UPDATE_ADDRESS, UPDATE_ADDRESS_SUCCESS, UPDATE_ADDRESS_FAIL],
    payload: {
      request: {
        method: "PUT",
        url: `/identity/api/address-book/${addressId}`,
        data
      }
    }
  };
}

export function removeAddress(addressId: string): AddressBookActionCreators {
  return {
    types: [REMOVE_ADDRESS, REMOVE_ADDRESS_SUCCESS, REMOVE_ADDRESS_FAIL],
    payload: {
      request: {
        method: "DELETE",
        url: `/identity/api/address-book/${addressId}`
      }
    }
  };
}
