import React from "react";
import renderer from "react-test-renderer";
import Password from "./Password";

it("renders without crashing", () => {
  const tree = renderer.create(<Password meta={{ touched: true }} />).toJSON();
  expect(tree).toMatchSnapshot();
});
