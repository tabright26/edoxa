import React from "react";
import renderer from "react-test-renderer";
import { Year } from ".";

it("renders without crashing", () => {
  const tree = renderer.create(<Year year={2030} />).toJSON();
  expect(tree).toMatchSnapshot();
});
