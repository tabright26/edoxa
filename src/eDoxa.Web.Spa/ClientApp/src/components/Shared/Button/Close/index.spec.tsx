import React from "react";
import renderer from "react-test-renderer";
import { Close } from ".";

it("renders without crashing", () => {
  const tree = renderer.create(<Close />).toJSON();
  expect(tree).toMatchSnapshot();
});