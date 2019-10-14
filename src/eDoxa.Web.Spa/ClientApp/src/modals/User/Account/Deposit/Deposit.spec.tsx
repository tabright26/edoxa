import React from "react";
import Deposit from "./Deposit";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const store: any = {
    getState: () => {
      return {
        modal: {
          name: ""
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };
  const tree = renderer
    .create(
      <Provider store={store}>
        <Deposit />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
