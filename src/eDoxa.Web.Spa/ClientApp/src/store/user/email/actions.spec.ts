import { confirmEmail } from "./actions";
import { CONFIRM_EMAIL, CONFIRM_EMAIL_SUCCESS, CONFIRM_EMAIL_FAIL } from "./types";

describe("identity actions", () => {
  it("should create an action to get confirm email", () => {
    const userId = "1";
    const code = "0";
    const expectedType = [CONFIRM_EMAIL, CONFIRM_EMAIL_SUCCESS, CONFIRM_EMAIL_FAIL];
    const expectedMethod = "GET";
    const expectedUrl = `/identity/api/email/confirm?userId=${userId}&code=${code}`;

    const object = confirmEmail(userId, code);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
  });
});
