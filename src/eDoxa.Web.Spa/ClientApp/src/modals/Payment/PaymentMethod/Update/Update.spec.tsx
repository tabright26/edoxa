import React from "react";
import Update from "./Update";
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
        <Update />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
