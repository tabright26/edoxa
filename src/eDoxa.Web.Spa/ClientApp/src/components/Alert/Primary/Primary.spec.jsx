import React from "react";
import renderer from "react-test-renderer";
import Primary from "./Primary";

it("renders without crashing", () => {
  const tree = renderer.create(<Primary />).toJSON();
  expect(tree).toMatchSnapshot();
});
