import { validate } from "./validate";

var mockValidUserPersonnalInfo = {
  firstName: "Gabriel-Jose",
  lastName: "Roy-Dupuis",
  year: "1995",
  month: "08",
  day: "04",
  gender: "Other"
};

var mockInvalidUserPersonnalInfo = {
  firstName: "gab",
  lastName: "r",
  year: "12",
  month: "001",
  day: "32",
  gender: ""
};

var mockEmptyUserPersonnalInfo = {
  firstName: null,
  lastName: null,
  year: null,
  month: null,
  day: null,
  gender: null
};

test("All to be valid", () => {
  const errors = validate(mockValidUserPersonnalInfo);
  expect(errors).not.toHaveProperty("firstName");
});

test("firstName to be invalid", () => {
  const errors = validate(mockInvalidUserPersonnalInfo);
  expect(errors).toHaveProperty("firstName", "Invalid first name");
});

test("firstName to be empty", () => {
  const errors = validate(mockEmptyUserPersonnalInfo);
  expect(errors).toHaveProperty("firstName", "First name is required");
});
