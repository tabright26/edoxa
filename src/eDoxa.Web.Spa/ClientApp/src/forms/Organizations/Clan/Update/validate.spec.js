import validate from "./validate";

var mockValidClan = {
  summary: "This is a summary."
};

var mockInvalidClan = {
  summary: "This."
};

test("All to be valid", () => {
  const errors = validate(mockValidClan);
  expect(errors).not.toHaveProperty("summary");
});

test("summary to be invalid", () => {
  const errors = validate(mockInvalidClan);
  expect(errors).toHaveProperty("summary", "Invalid summary");
});
