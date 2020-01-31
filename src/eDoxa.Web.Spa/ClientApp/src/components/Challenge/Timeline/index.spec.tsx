import React from "react";
import renderer from "react-test-renderer";
import Timeline from ".";

it("renders without crashing", () => {
  // Act
  const tree = renderer.create(<Timeline />).toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
