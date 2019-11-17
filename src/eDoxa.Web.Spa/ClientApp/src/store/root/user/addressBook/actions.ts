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
  UPDATE_USER_ADDRESS_FAIL,
  UserAddressBookActionCreators
} from "./types";
import { AddressId } from "types";

export function loadUserAddressBook(): UserAddressBookActionCreators {
  return {
    types: [LOAD_USER_ADDRESSBOOK, LOAD_USER_ADDRESSBOOK_SUCCESS, LOAD_USER_ADDRESSBOOK_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/identity/api/address-book`
      }
    }
  };
}

export function createUserAddress(data: any): UserAddressBookActionCreators {
  return {
    types: [CREATE_USER_ADDRESS, CREATE_USER_ADDRESS_SUCCESS, CREATE_USER_ADDRESS_FAIL],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/address-book",
        data
      }
    }
  };
}

export function updateUserAddress(addressId: AddressId, data: any): UserAddressBookActionCreators {
  return {
    types: [UPDATE_USER_ADDRESS, UPDATE_USER_ADDRESS_SUCCESS, UPDATE_USER_ADDRESS_FAIL],
    payload: {
      request: {
        method: "PUT",
        url: `/identity/api/address-book/${addressId}`,
        data
      }
    }
  };
}

export function deleteUserAddress(addressId: AddressId): UserAddressBookActionCreators {
  return {
    types: [DELETE_USER_ADDRESS, DELETE_USER_ADDRESS_SUCCESS, DELETE_USER_ADDRESS_FAIL],
    payload: {
      request: {
        method: "DELETE",
        url: `/identity/api/address-book/${addressId}`
      }
    }
  };
}
