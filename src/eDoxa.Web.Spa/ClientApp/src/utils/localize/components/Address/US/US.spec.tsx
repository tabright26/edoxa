import React from "react";
import renderer from "react-test-renderer";
import US from "./US";

it("renders without crashing", () => {
  const tree = renderer.create(<US />).toJSON();
  expect(tree).toMatchSnapshot();
});
