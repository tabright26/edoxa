import { validate } from "./validate";

var mockValidResetPassword = {
  email: "gabriel@edoxa.gg",
  password: "Testing123!"
};

var mockInvalidResetPassword = {
  email: "gabriel.roy",
  password: "pass"
};

var mockInvalidResetPasswordUppercase = {
  email: "gabriel@edoxa.gg",
  password: "shorting123"
};

var mockInvalidResetPasswordLength = {
  email: "gabriel@edoxa.gg",
  password: "short"
};

var mockInvalidResetPasswordSpecial = {
  email: "gabriel@edoxa.gg",
  password: "Shorting123"
};

var mockEmptyResetPassword = {
  email: null,
  password: null
};

test("All to be valid", () => {
  const errors = validate(mockValidResetPassword);
  expect(errors).not.toHaveProperty("email");
  expect(errors).not.toHaveProperty("password");
});

test("email to be invalid", () => {
  const errors = validate(mockInvalidResetPassword);
  expect(errors).toHaveProperty("email", "Invalid email");
});

test("password to be invalid", () => {
  const errors = validate(mockInvalidResetPassword);
  expect(errors).toHaveProperty("password", "Invalid password");
});

test("password to be invalid", () => {
  const errors = validate(mockInvalidResetPasswordUppercase);
  expect(errors).toHaveProperty("password", "Invalid password");
});

test("password to be invalid", () => {
  const errors = validate(mockInvalidResetPasswordLength);
  expect(errors).toHaveProperty("password", "Invalid password");
});

test("password to be invalid", () => {
  const errors = validate(mockInvalidResetPasswordSpecial);
  expect(errors).toHaveProperty("password", "Invalid password");
});

test("All to be empty", () => {
  const errors = validate(mockEmptyResetPassword);
  expect(errors).toHaveProperty("email", "Email is required");
  expect(errors).toHaveProperty("password", "Password is required");
});
