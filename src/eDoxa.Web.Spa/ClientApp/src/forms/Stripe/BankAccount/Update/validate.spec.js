import validate from "./validate";

var mockValidBankUpdate = {
  account_holder_name: "Gabriel Roy"
};

var mockInvalidBankUpdate = {
  account_holder_name: "Gabri1"
};

var mockEmptyBankUpdate = {
  account_holder_name: null
};

test("All to be valid", () => {
  const errors = validate(mockValidBankUpdate);
  expect(errors).not.toHaveProperty("account_holder_name");
});

test("account_holder_name to be invalid", () => {
  const errors = validate(mockInvalidBankUpdate);
  expect(errors).toHaveProperty("account_holder_name", "Invalid account holder name");
});

test("All to be empty", () => {
  const errors = validate(mockEmptyBankUpdate);
  expect(errors).toHaveProperty("account_holder_name", "Account holder card name is required");
});
