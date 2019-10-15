import React from "react";
import renderer from "react-test-renderer";
import No from "./No";

it("renders without crashing", () => {
  const tree = renderer.create(<No />).toJSON();
  expect(tree).toMatchSnapshot();
});
