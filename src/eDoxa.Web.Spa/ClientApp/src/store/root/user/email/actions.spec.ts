import { confirmUserEmail } from "./actions";
import { CONFIRM_USER_EMAIL, CONFIRM_USER_EMAIL_SUCCESS, CONFIRM_USER_EMAIL_FAIL } from "./types";

describe("identity actions", () => {
  it("should create an action to get confirm email", () => {
    const userId = "1";
    const code = "0";
    const expectedType = [CONFIRM_USER_EMAIL, CONFIRM_USER_EMAIL_SUCCESS, CONFIRM_USER_EMAIL_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/identity/api/email/confirm?userId=${userId}&code=${code}`;

    const object = confirmUserEmail(userId, code);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });
});
