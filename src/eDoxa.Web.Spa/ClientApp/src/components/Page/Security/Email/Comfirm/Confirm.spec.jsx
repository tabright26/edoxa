import React from "react";
import renderer from "react-test-renderer";
import Confirm from "./Confirm";

it("renders without crashing", () => {
  const tree = renderer.create(<Confirm />).toJSON();
  expect(tree).toMatchSnapshot();
});
