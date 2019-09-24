import React from "react";
import renderer from "react-test-renderer";
import Expiration from "./Expiration";

it("renders without crashing", () => {
  const tree = renderer.create(<Expiration month={1} year={2030} />).toJSON();
  expect(tree).toMatchSnapshot();
});
