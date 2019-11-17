import React from "react";
import renderer from "react-test-renderer";
import Unknown from "./Unknown";

it("renders without crashing", () => {
  const tree = renderer.create(<Unknown />).toJSON();
  expect(tree).toMatchSnapshot();
});
