import React from "react";
import renderer from "react-test-renderer";
import Month from "./Month";

it("renders without crashing", () => {
  const tree = renderer.create(<Month month={10} />).toJSON();
  expect(tree).toMatchSnapshot();
});
