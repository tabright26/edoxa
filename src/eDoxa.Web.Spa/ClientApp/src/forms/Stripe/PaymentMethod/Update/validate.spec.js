import validate from "./validate";

var mockValidCardUpdate = {
  ccMonth: "12",
  ccYear: "2021"
};

var mockInvalidCardUpdate = {
  ccMonth: "0",
  ccYear: "2008"
};

var mockEmptyCardUpdate = {
  ccMonth: null,
  ccYear: null
};

test("All to be valid", () => {
  const errors = validate(mockValidCardUpdate);
  expect(errors).not.toHaveProperty("ccMonth");
  expect(errors).not.toHaveProperty("ccYear");
});

test("ccMonth to be invalid", () => {
  const errors = validate(mockInvalidCardUpdate);
  expect(errors).toHaveProperty("ccMonth", "Invalid credit card expiration month");
});

test("ccYear to be invalid", () => {
  const errors = validate(mockInvalidCardUpdate);
  expect(errors).toHaveProperty("ccYear", "Invalid credit card expiration year");
});

test("All to be empty", () => {
  const errors = validate(mockEmptyCardUpdate);
  expect(errors).toHaveProperty("ccMonth", "Credit card expiration month is required");
  expect(errors).toHaveProperty("ccYear", "Credit card expiration year is required");
});
