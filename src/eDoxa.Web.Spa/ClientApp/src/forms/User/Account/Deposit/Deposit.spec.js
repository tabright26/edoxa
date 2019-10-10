import React from "react";
import Deposit from "./Deposit";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const tree = renderer
    .create(
      <Provider store={{ getState: () => {}, dispatch: action => {}, subscribe: () => {} }}>
        <Deposit initialValues={{ amounts: [] }} />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
