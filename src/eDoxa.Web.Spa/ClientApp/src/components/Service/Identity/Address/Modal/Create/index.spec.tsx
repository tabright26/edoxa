import React from "react";
import Create from ".";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import store from "store";

it("renders correctly", () => {
  const tree = renderer
    .create(
      <Provider store={store}>
        <Create />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
