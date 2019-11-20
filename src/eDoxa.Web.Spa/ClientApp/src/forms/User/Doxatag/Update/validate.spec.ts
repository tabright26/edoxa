import { validate } from "./validate";

var mockValidDoxatag = {
  name: "gabTheVegetoy"
};

var mockInvalidDoxatag = {
  name: "_yoMonsieur"
};

var mockEmptyDoxatag = {
  name: null
};

test("name to be valid", () => {
  const errors = validate(mockValidDoxatag);
  expect(errors).not.toHaveProperty("name");
});

test("name to be invalid", () => {
  const errors = validate(mockInvalidDoxatag);
  expect(errors).toHaveProperty("name", "Invalid format. Must between 16 characters and greater than characters 2");
});

test("name to be empty", () => {
  const errors = validate(mockEmptyDoxatag);
  expect(errors).toHaveProperty("name", "Doxatag is required");
});
