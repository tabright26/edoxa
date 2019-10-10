import validate from "./validate";

var mockValidClan = {
  name: "BestClanMate"
};

var mockInvalidClan = {
  name: "oui_monsieur"
};

var mockEmptyClan = {
  name: null
};

test("name to be valid", () => {
  const errors = validate(mockValidClan);
  expect(errors).not.toHaveProperty("name");
});

test("name to be invalid", () => {
  const errors = validate(mockInvalidClan);
  expect(errors).toHaveProperty("name", "Invalid format. Must between 3-20 characters and alphanumeric. Hyphens, spaces, dot and coma allowed.");
});

test("name to be empty", () => {
  const errors = validate(mockEmptyClan);
  expect(errors).toHaveProperty("name", "Name is required");
});
