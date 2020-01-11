import React from "react";
import Create from "./Create";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { CREATE_USER_ADDRESS_MODAL } from "utils/modal/constants";

it("renders correctly", () => {
  const store: any = {
    getState: () => {
      return {
        modal: {
          name: CREATE_USER_ADDRESS_MODAL
        }
      };
    },
    dispatch: action => {},
    subscribe: () => {}
  };
  const tree = renderer
    .create(
      <Provider store={store}>
        <Create />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
