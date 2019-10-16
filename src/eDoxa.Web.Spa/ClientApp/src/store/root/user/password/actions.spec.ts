import { forgotPassword, resetPassword } from "./actions";
import { FORGOT_USER_PASSWORD, FORGOT_USER_PASSWORD_SUCCESS, FORGOT_USER_PASSWORD_FAIL, RESET_USER_PASSWORD, RESET_USER_PASSWORD_SUCCESS, RESET_USER_PASSWORD_FAIL } from "./types";

describe("identity actions", () => {
  it("should create an action to post user forgot password", () => {
    const expectedType = [FORGOT_USER_PASSWORD, FORGOT_USER_PASSWORD_SUCCESS, FORGOT_USER_PASSWORD_FAIL];
    const expectedMethod = "POST";
    const expectedUrl = "/identity/api/password/forgot";
    const expectedEmail = { email: "gab@edoxa.gg" };

    const object = forgotPassword(expectedEmail);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedEmail);
  });

  it("should create an action to post user reset password", () => {
    const expectedType = [RESET_USER_PASSWORD, RESET_USER_PASSWORD_SUCCESS, RESET_USER_PASSWORD_FAIL];
    const expectedMethod = "POST";
    const expectedUrl = "/identity/api/password/reset";
    const expectedEmail = { email: "gab@edoxa.gg", password: "password" };

    const object = resetPassword(expectedEmail);

    expect(object.types).toEqual(expectedType);
    expect(object.payload.request.method).toEqual(expectedMethod);
    expect(object.payload.request.url).toEqual(expectedUrl);
    expect(object.payload.request.data).toEqual(expectedEmail);
  });
});
