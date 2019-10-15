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
  expect(errors).not.toHaveProperty("lastName");
  expect(errors).not.toHaveProperty("year");
  expect(errors).not.toHaveProperty("month");
  expect(errors).not.toHaveProperty("day");
  expect(errors).not.toHaveProperty("gender");
});

test("firstName to be invalid", () => {
  const errors = validate(mockInvalidUserPersonnalInfo);
  expect(errors).toHaveProperty("firstName", "Invalid first name");
});

test("lastName to be invalid", () => {
  const errors = validate(mockInvalidUserPersonnalInfo);
  expect(errors).toHaveProperty("lastName", "Invalid last name");
});

test("year to be invalid", () => {
  const errors = validate(mockInvalidUserPersonnalInfo);
  expect(errors).toHaveProperty("year", "Invalid year");
});

test("month to be invalid", () => {
  const errors = validate(mockInvalidUserPersonnalInfo);
  expect(errors).toHaveProperty("month", "Invalid month");
});

test("day to be invalid", () => {
  const errors = validate(mockInvalidUserPersonnalInfo);
  expect(errors).toHaveProperty("day", "Invalid day");
});

test("All to be empty", () => {
  const errors = validate(mockEmptyUserPersonnalInfo);
  expect(errors).toHaveProperty("firstName", "First name is required");
  expect(errors).toHaveProperty("lastName", "Last name is required");
  expect(errors).toHaveProperty("year", "Year of birth is required");
  expect(errors).toHaveProperty("month", "Month of birth is required");
  expect(errors).toHaveProperty("day", "Day of birth is required");
  expect(errors).toHaveProperty("gender", "Gender is required");
});
