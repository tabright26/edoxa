import React from "react";
import renderer from "react-test-renderer";
import { Exp } from ".";

it("renders without crashing", () => {
  const tree = renderer.create(<Exp month={1} year={2030} />).toJSON();
  expect(tree).toMatchSnapshot();
});
