import validate from "./validate";

var mockValidAddress = {
  country: "Canada",
  line1: "4140 av.Kindersley",
  line2: null,
  city: "Montreal",
  state: "Quebec",
  postalCode: "H4P1K8"
};

var mockInvalidAddress = {
  country: "Americo",
  line1: "*_Test",
  line2: "*",
  city: "*",
  state: "@#$",
  postalCode: "@#$"
};

var mockEmptyAddress = {
  country: null,
  line1: null,
  line2: null,
  city: null,
  state: null,
  postalCode: null
};

test("All to be valid", () => {
  const errors = validate(mockValidAddress);
  expect(errors).not.toHaveProperty("CA");
  expect(errors).not.toHaveProperty("line1");
  expect(errors).not.toHaveProperty("line2");
  expect(errors).not.toHaveProperty("city");
  expect(errors).not.toHaveProperty("state");
  expect(errors).not.toHaveProperty("postalCode");
});

test("country to be invalid", () => {
  const errors = validate(mockInvalidAddress);
  expect(errors).toHaveProperty("country", "Invalid country");
});

test("line1 to be invalid", () => {
  const errors = validate(mockInvalidAddress);
  expect(errors).toHaveProperty("line1", "Invalid main address");
});

test("line2 to be invalid", () => {
  const errors = validate(mockInvalidAddress);
  expect(errors).toHaveProperty("line2", "Invalid secondary address");
});

test("city to be invalid", () => {
  const errors = validate(mockInvalidAddress);
  expect(errors).toHaveProperty("city", "Invalid city");
});

test("state to be invalid", () => {
  const errors = validate(mockInvalidAddress);
  expect(errors).toHaveProperty("state", "Invalid state");
});

test("postalCode to be invalid", () => {
  const errors = validate(mockInvalidAddress);
  expect(errors).toHaveProperty("postalCode", "Invalid postal code");
});

test("All to be empty", () => {
  const errors = validate(mockEmptyAddress);
  expect(errors).toHaveProperty("country", "Country is required");
  expect(errors).toHaveProperty("line1", "Main address is required");
  expect(errors).toHaveProperty("city", "City is required");
});
