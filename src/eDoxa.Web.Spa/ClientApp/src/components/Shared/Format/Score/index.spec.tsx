import React from "react";
import renderer from "react-test-renderer";
import { Score } from ".";

it("renders without crashing", () => {
  const tree = renderer.create(<Score score={100} />).toJSON();
  expect(tree).toMatchSnapshot();
});
