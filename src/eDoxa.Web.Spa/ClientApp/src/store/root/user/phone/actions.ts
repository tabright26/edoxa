import {
  LOAD_USER_PHONE,
  LOAD_USER_PHONE_SUCCESS,
  LOAD_USER_PHONE_FAIL,
  UPDATE_USER_PHONE,
  UPDATE_USER_PHONE_SUCCESS,
  UPDATE_USER_PHONE_FAIL,
  UserPhoneActionCreators
} from "./types";

export function loadUserPhone(): UserPhoneActionCreators {
  return {
    types: [LOAD_USER_PHONE, LOAD_USER_PHONE_SUCCESS, LOAD_USER_PHONE_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/identity/api/phone"
      }
    }
  };
}

export function updateUserPhone(data: any, meta: any): UserPhoneActionCreators {
  return {
    types: [
      UPDATE_USER_PHONE,
      UPDATE_USER_PHONE_SUCCESS,
      UPDATE_USER_PHONE_FAIL
    ],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/phone",
        data
      }
    },
    meta
  };
}
