import React from "react";
import renderer from "react-test-renderer";
import Select from "./Select";

it("renders without crashing", () => {
  const tree = renderer.create(<Select meta={{ touched: true }} />).toJSON();
  expect(tree).toMatchSnapshot();
});
