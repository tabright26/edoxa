import { validate } from "./validate";

var mockValidEmail = {
  email: "gab@edoxa.gg"
};

var mockInvalidEmail = {
  email: "123.com"
};

var mockEmptyEmail = {
  email: null
};

test("email to be valid", () => {
  const errors = validate(mockValidEmail);
  expect(errors).not.toHaveProperty("email");
});

test("email to be invalid", () => {
  const errors = validate(mockInvalidEmail);
  expect(errors).toHaveProperty("email", "Invalid email");
});

test("email to be empty", () => {
  const errors = validate(mockEmptyEmail);
  expect(errors).toHaveProperty("email", "Email is required");
});
