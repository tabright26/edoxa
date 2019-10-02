import React from "react";
import Delete from "./Delete";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const tree = renderer
    .create(
      <Provider store={{ getState: () => {}, dispatch: action => {}, subscribe: () => {} }}>
        <Delete />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
