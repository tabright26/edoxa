import React from "react";
import renderer from "react-test-renderer";
import Default from "./Default";

it("renders without crashing", () => {
  const tree = renderer.create(<Default />).toJSON();
  expect(tree).toMatchSnapshot();
});
