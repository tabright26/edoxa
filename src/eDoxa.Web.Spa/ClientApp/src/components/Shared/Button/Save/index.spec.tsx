import React from "react";
import renderer from "react-test-renderer";
import { Save } from ".";

it("renders without crashing", () => {
  const tree = renderer.create(<Save />).toJSON();
  expect(tree).toMatchSnapshot();
});
