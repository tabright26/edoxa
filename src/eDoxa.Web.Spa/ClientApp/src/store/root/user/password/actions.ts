import { FORGOT_PASSWORD, FORGOT_PASSWORD_SUCCESS, FORGOT_PASSWORD_FAIL, RESET_PASSWORD, RESET_PASSWORD_SUCCESS, RESET_PASSWORD_FAIL, PasswordActionCreators } from "./types";

export function forgotPassword(data: any): PasswordActionCreators {
  return {
    types: [FORGOT_PASSWORD, FORGOT_PASSWORD_SUCCESS, FORGOT_PASSWORD_FAIL],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/password/forgot",
        data
      }
    }
  };
}

export function resetPassword(data: any): PasswordActionCreators {
  return {
    types: [RESET_PASSWORD, RESET_PASSWORD_SUCCESS, RESET_PASSWORD_FAIL],
    payload: {
      request: {
        method: "POST",
        url: "/identity/api/password/reset",
        data
      }
    }
  };
}
