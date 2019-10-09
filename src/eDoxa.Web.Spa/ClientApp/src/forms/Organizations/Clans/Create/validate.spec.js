import validate from "./validate";

var mockValidClan = {
  name: "TestClan",
  summary: "This is a summary."
};

// var mockInvalidClan = {
//   name: "Ts",
//   summary: "This."
// };

// var mockEmptyClan = {
//   name: null,
//   summary: null
// };

test("All to be valid", () => {
  const errors = validate(mockValidClan);
  expect(errors).not.toHaveProperty("name");
  expect(errors).not.toHaveProperty("summary");
});

// test("name to be invalid", () => {
//   const errors = validate(mockInvalidClan);
//   expect(errors).toHaveProperty("name", "Invalid name");
// });

// test("summary to be invalid", () => {
//   const errors = validate(mockInvalidClan);
//   expect(errors).toHaveProperty("summary", "Invalid summary");
// });

// test("All to be empty", () => {
//   const errors = validate(mockEmptyClan);
//   expect(errors).toHaveProperty("name", "Name is required");
// });
