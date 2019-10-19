import React from "react";
import Deposit from "./Deposit";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { Bundle } from "types";

it("renders correctly", () => {
  const bundles: Bundle[] = [];
  const store: any = {
    getState: () => {},
    dispatch: action => {},
    subscribe: () => {}
  };
  const tree = renderer
    .create(
      <Provider store={store}>
        <Deposit bundles={bundles} />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
