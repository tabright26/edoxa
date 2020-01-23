import React from "react";
import renderer from "react-test-renderer";
import Icon from ".";

it("renders without crashing", () => {
  const tree = renderer.create(<Icon brand="amex" />).toJSON();
  expect(tree).toMatchSnapshot();
});
