import React from "react";
import renderer from "react-test-renderer";
import { Loading } from ".";

it("renders without crashing", () => {
  const tree = renderer.create(<Loading />).toJSON();
  expect(tree).toMatchSnapshot();
});
