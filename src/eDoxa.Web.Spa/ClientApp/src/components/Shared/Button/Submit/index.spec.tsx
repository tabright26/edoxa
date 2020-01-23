import React from "react";
import renderer from "react-test-renderer";
import { Submit } from ".";

it("renders without crashing", () => {
  const tree = renderer.create(<Submit />).toJSON();
  expect(tree).toMatchSnapshot();
});
