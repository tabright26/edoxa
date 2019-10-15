import { LOAD_PHONENUMBER, LOAD_PHONENUMBER_SUCCESS, LOAD_PHONENUMBER_FAIL, CHANGE_PHONENUMBER, CHANGE_PHONENUMBER_SUCCESS, CHANGE_PHONENUMBER_FAIL, PhoneNumberActionCreators } from "./types";

export function loadPhoneNumber(): PhoneNumberActionCreators {
  return {
    types: [LOAD_PHONENUMBER, LOAD_PHONENUMBER_SUCCESS, LOAD_PHONENUMBER_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/identity/api/phone-number"
      }
    }
  };
}

export function changePhoneNumber(phoneNumber: string): PhoneNumberActionCreators {
  return {
    types: [CHANGE_PHONENUMBER, CHANGE_PHONENUMBER_SUCCESS, CHANGE_PHONENUMBER_FAIL],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/phone-number",
        data: {
          phoneNumber
        }
      }
    }
  };
}
