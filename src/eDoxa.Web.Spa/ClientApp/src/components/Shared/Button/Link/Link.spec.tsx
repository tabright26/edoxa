import React from "react";
import renderer from "react-test-renderer";
import Link from "./Link";

it("renders without crashing", () => {
  const tree = renderer.create(<Link />).toJSON();
  expect(tree).toMatchSnapshot();
});