import React from "react";
import Register from "./Register";
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
        <Register />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
