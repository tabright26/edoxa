import React from "react";
import renderer from "react-test-renderer";
import { Currency } from ".";
import { CURRENCY_TYPE_MONEY } from "types";

it("renders without crashing", () => {
  const tree = renderer
    .create(<Currency currency={{ type: CURRENCY_TYPE_MONEY, amount: 100 }} />)
    .toJSON();
  expect(tree).toMatchSnapshot();
});
