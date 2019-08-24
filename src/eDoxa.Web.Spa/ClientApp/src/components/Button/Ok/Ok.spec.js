import React from "react";
import renderer from "react-test-renderer";
import Ok from "./Ok";

it("renders without crashing", () => {
  const tree = renderer.create(<Ok />).toJSON();
  expect(tree).toMatchSnapshot();
});
