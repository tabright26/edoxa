import React from "react";
import renderer from "react-test-renderer";
import { Verified } from ".";

it("renders without crashing", () => {
  const tree = renderer
    .create(<Verified verified={false} />)
    .toJSON();
  expect(tree).toMatchSnapshot();
});
