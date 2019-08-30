import validate from "./validate";

var mockValidForgotPassword = {
  email: "gabriel@edoxa.gg"
};

var mockInvalidForgotPassword = {
  email: "gabriel.roy"
};

var mockEmptyForgotPassword = {
  email: null
};

test("All to be valid", () => {
  const errors = validate(mockValidForgotPassword);
  expect(errors).not.toHaveProperty("email");
});

test("email to be invalid", () => {
  const errors = validate(mockInvalidForgotPassword);
  expect(errors).toHaveProperty("email", "Invalid email");
});

test("All to be empty", () => {
  const errors = validate(mockEmptyForgotPassword);
  expect(errors).toHaveProperty("email", "Email is required");
});
