import React from "react";
import renderer from "react-test-renderer";
import Verification from "./Verification";

it("renders without crashing", () => {
  const tree = renderer.create(<Verification verified={false} className={""} />).toJSON();
  expect(tree).toMatchSnapshot();
});
