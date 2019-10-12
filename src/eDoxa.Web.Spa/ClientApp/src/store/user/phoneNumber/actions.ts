import { LOAD_PHONENUMBER, LOAD_PHONENUMBER_SUCCESS, LOAD_PHONENUMBER_FAIL, PhoneNumberActionCreators } from "./types";

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
