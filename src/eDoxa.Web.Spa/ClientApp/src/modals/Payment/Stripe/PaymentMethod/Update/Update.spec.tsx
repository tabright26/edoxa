import React from "react";
import Update from "./Update";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { UPDATE_PAYMENTMETHOD_MODAL } from "modals";

it("renders correctly", () => {
  const store: any = {
    getState: () => {
      return {
        modal: {
          name: UPDATE_PAYMENTMETHOD_MODAL
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };
  const tree = renderer
    .create(
      <Provider store={store}>
        <Update />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
