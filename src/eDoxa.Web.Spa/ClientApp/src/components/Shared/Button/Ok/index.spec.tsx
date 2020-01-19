import React from "react";
import renderer from "react-test-renderer";
import { Ok } from ".";

it("renders without crashing", () => {
  const tree = renderer.create(<Ok />).toJSON();
  expect(tree).toMatchSnapshot();
});
