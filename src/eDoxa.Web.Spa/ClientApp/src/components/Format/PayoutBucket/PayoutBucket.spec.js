import React from "react";
import renderer from "react-test-renderer";
import PayoutBucket from "./PayoutBucket";

it("renders without crashing", () => {
  const tree = renderer.create(<PayoutBucket />).toJSON();
  expect(tree).toMatchSnapshot();
});
