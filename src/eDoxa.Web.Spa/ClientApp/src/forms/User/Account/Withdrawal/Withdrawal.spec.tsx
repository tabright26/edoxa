import React from "react";
import Withdrawal from "./Withdrawal";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const store: any = {
    getState: () => {},
    dispatch: action => {},
    subscribe: () => {}
  };
  const tree = renderer
    .create(
      <Provider store={store}>
        <Withdrawal initialValues={{ amounts: [] }} />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
