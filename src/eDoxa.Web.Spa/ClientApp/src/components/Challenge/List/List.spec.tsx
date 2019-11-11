import React from "react";
import renderer from "react-test-renderer";
import List from "./List";

it("renders without crashing", () => {
  // Act
  const tree = renderer.create(<List />).toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
