import React from "react";
import renderer from "react-test-renderer";
import Option from "./Option";

it("renders without crashing", () => {
  const tree = renderer.create(<Option year={30} />).toJSON();
  expect(tree).toMatchSnapshot();
});
