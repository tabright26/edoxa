import React from "react";
import renderer from "react-test-renderer";
import BrandIcon from "./BrandIcon";

it("renders without crashing", () => {
  const tree = renderer.create(<BrandIcon brand="amex" />).toJSON();
  expect(tree).toMatchSnapshot();
});
