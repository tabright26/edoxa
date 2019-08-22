import React from "react";
import Text from ".";
import renderer from "react-test-renderer";

it("renders correctly", () => {
  const tree = renderer.create(<Text meta={{ touched: true }} />).toJSON();
  expect(tree).toMatchSnapshot();
});
