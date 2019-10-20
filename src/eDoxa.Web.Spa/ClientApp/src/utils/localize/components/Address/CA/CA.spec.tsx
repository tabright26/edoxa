import React from "react";
import renderer from "react-test-renderer";
import CA from "./CA";

it("renders without crashing", () => {
  const tree = renderer.create(<CA />).toJSON();
  expect(tree).toMatchSnapshot();
});
