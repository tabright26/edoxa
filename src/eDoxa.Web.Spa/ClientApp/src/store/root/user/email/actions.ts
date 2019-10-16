import { LOAD_USER_EMAIL, LOAD_USER_EMAIL_SUCCESS, LOAD_USER_EMAIL_FAIL, CONFIRM_USER_EMAIL, CONFIRM_USER_EMAIL_SUCCESS, CONFIRM_USER_EMAIL_FAIL, UserEmailActionCreators } from "./types";

export function loadEmail(): UserEmailActionCreators {
  return {
    types: [LOAD_USER_EMAIL, LOAD_USER_EMAIL_SUCCESS, LOAD_USER_EMAIL_FAIL],
    payload: {
      request: {
        method: "GET",
        url: "/identity/api/email"
      }
    }
  };
}

export function confirmEmail(userId: string, code: string): UserEmailActionCreators {
  return {
    types: [CONFIRM_USER_EMAIL, CONFIRM_USER_EMAIL_SUCCESS, CONFIRM_USER_EMAIL_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/identity/api/email/confirm?userId=${userId}&code=${code}`
      }
    }
  };
}
