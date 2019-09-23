import { CONFIRM_EMAIL, CONFIRM_EMAIL_SUCCESS, CONFIRM_EMAIL_FAIL, EmailActionCreators } from "./types";

export function confirmEmail(userId: string, code: string): EmailActionCreators {
  return {
    types: [CONFIRM_EMAIL, CONFIRM_EMAIL_SUCCESS, CONFIRM_EMAIL_FAIL],
    payload: {
      request: {
        method: "GET",
        url: `/identity/api/email/confirm?userId=${userId}&code=${code}`
      }
    }
  };
}
