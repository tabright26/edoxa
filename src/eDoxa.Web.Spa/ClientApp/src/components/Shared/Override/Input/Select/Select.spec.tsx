import React from "react";
import renderer from "react-test-renderer";
import Select from "./Select";

it("renders without crashing", () => {
  const meta: any = { touched: true };
  const tree = renderer.create(<Select meta={meta} />).toJSON();
  expect(tree).toMatchSnapshot();
});
