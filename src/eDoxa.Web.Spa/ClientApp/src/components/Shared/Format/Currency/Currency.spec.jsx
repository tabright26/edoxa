import React from "react";
import renderer from "react-test-renderer";
import Currency from "./Currency";

it("renders without crashing", () => {
  const tree = renderer.create(<Currency />).toJSON();
  expect(tree).toMatchSnapshot();
});
