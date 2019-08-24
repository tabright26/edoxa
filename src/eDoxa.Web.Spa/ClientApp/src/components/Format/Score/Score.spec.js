import React from "react";
import renderer from "react-test-renderer";
import Score from "./Score";

it("renders without crashing", () => {
  const tree = renderer.create(<Score />).toJSON();
  expect(tree).toMatchSnapshot();
});
