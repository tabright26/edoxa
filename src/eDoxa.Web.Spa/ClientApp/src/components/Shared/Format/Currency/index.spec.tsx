import React from "react";
import renderer from "react-test-renderer";
import { Currency } from ".";

it("renders without crashing", () => {
  const tree = renderer.create(<Currency currency="token" amount={10000} />).toJSON();
  expect(tree).toMatchSnapshot();
});
