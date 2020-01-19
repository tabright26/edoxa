import React from "react";
import renderer from "react-test-renderer";
import { Cancel } from ".";

it("renders without crashing", () => {
  const tree = renderer.create(<Cancel />).toJSON();
  expect(tree).toMatchSnapshot();
});
