import React from "react";
import renderer from "react-test-renderer";
import Option from "./Option";

it("renders without crashing", () => {
  const tree = renderer.create(<Option month={10} />).toJSON();
  expect(tree).toMatchSnapshot();
});
