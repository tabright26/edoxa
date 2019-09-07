import React from "react";
import renderer from "react-test-renderer";
import Yes from "./Yes";

it("renders without crashing", () => {
  const tree = renderer.create(<Yes />).toJSON();
  expect(tree).toMatchSnapshot();
});
