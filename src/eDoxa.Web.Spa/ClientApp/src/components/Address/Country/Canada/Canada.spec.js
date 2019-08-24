import React from "react";
import renderer from "react-test-renderer";
import Canada from "./Canada";

it("renders without crashing", () => {
  const tree = renderer.create(<Canada />).toJSON();
  expect(tree).toMatchSnapshot();
});
