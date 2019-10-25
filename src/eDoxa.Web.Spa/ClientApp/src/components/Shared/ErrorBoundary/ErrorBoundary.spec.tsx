import React from "react";
import renderer from "react-test-renderer";
import ErrorBoundary from "./ErrorBoundary";

it("renders without crashing", () => {
  const tree = renderer.create(<ErrorBoundary />).toJSON();
  expect(tree).toMatchSnapshot();
});
