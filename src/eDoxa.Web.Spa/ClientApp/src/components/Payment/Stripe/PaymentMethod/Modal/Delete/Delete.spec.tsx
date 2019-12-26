import React from "react";
import Delete from "./Delete";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { DELETE_STRIPE_PAYMENTMETHOD_MODAL } from "modals";

it("renders correctly", () => {
  const store: any = {
    getState: () => {
      return {
        modal: {
          name: DELETE_STRIPE_PAYMENTMETHOD_MODAL
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };
  const tree = renderer
    .create(
      <Provider store={store}>
        <Delete />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
