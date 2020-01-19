import React from "react";
import renderer from "react-test-renderer";
import { PayoutBucket } from ".";

it("renders without crashing", () => {
  const tree = renderer.create(<PayoutBucket />).toJSON();
  expect(tree).toMatchSnapshot();
});
