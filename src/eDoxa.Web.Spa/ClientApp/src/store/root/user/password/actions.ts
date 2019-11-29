import {
  FORGOT_USER_PASSWORD,
  FORGOT_USER_PASSWORD_SUCCESS,
  FORGOT_USER_PASSWORD_FAIL,
  RESET_USER_PASSWORD,
  RESET_USER_PASSWORD_SUCCESS,
  RESET_USER_PASSWORD_FAIL,
  UserPasswordActionCreators
} from "./types";

export function forgotUserPassword(
  data: any,
  meta: any
): UserPasswordActionCreators {
  return {
    types: [
      FORGOT_USER_PASSWORD,
      FORGOT_USER_PASSWORD_SUCCESS,
      FORGOT_USER_PASSWORD_FAIL
    ],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/password/forgot",
        data
      }
    },
    meta
  };
}

export function resetUserPassword(
  data: any,
  meta: any
): UserPasswordActionCreators {
  return {
    types: [
      RESET_USER_PASSWORD,
      RESET_USER_PASSWORD_SUCCESS,
      RESET_USER_PASSWORD_FAIL
    ],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/password/reset",
        data
      }
    },
    meta
  };
}
