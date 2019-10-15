import { validate } from "./validate";

var mockValidDoxaTag = {
  name: "gabTheVegetoy"
};

var mockInvalidDoxaTag = {
  name: "_yoMonsieur"
};

var mockEmptyDoxaTag = {
  name: null
};

test("name to be valid", () => {
  const errors = validate(mockValidDoxaTag);
  expect(errors).not.toHaveProperty("name");
});

test("name to be invalid", () => {
  const errors = validate(mockInvalidDoxaTag);
  expect(errors).toHaveProperty("name", "Invalid format. Must between 16 characters and greater than characters 2");
});

test("name to be empty", () => {
  const errors = validate(mockEmptyDoxaTag);
  expect(errors).toHaveProperty("name", "DoxaTag is required");
});
