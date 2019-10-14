import React from "react";
import Withdrawal from "./Withdrawal";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { WITHDRAWAL_MODAL } from "modals";

it("renders correctly", () => {
  const store: any = {
    getState: () => {
      return {
        modal: {
          name: WITHDRAWAL_MODAL
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };
  const tree = renderer
    .create(
      <Provider store={store}>
        <Withdrawal />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
