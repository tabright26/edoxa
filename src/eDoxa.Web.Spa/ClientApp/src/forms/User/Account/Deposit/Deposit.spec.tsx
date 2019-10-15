import React from "react";
import Deposit from "./Deposit";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const initialValues: any = {
    amounts: []
  };
  const store: any = {
    getState: () => {},
    dispatch: action => {},
    subscribe: () => {}
  };
  const tree = renderer
    .create(
      <Provider store={store}>
        <Deposit initialValues={initialValues} />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
