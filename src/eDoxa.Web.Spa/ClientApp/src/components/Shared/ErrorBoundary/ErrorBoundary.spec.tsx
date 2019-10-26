import React from "react";
import renderer from "react-test-renderer";
import ErrorBoundary from "./ErrorBoundary";

it("renders without crashing", () => {
  const Component = () => (
    <ErrorBoundary>
      <span>Test</span>
    </ErrorBoundary>
  );

  const tree = renderer.create(<Component />).toJSON();

  expect(tree).toMatchSnapshot();
});
