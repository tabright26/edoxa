import React from "react";
import renderer from "react-test-renderer";
import Reset from "./Reset";

it("renders without crashing", () => {
  const tree = renderer.create(<Reset />).toJSON();
  expect(tree).toMatchSnapshot();
});
