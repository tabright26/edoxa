import React from "react";
import renderer from "react-test-renderer";
import { Text } from ".";

it("renders without crashing", () => {
  const tree = renderer.create(<Text meta={{ touched: true }} />).toJSON();
  expect(tree).toMatchSnapshot();
});
