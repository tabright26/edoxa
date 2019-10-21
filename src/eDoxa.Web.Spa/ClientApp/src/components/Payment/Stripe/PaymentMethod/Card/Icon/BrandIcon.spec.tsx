import React from "react";
import renderer from "react-test-renderer";
import BrandIcon from "./Icon";

it("renders without crashing", () => {
  const tree = renderer.create(<BrandIcon brand="amex" />).toJSON();
  expect(tree).toMatchSnapshot();
});
