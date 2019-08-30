import React from "react";
import renderer from "react-test-renderer";
import Forgot from "./Forgot";

it("renders without crashing", () => {
  const tree = renderer.create(<Forgot />).toJSON();
  expect(tree).toMatchSnapshot();
});
