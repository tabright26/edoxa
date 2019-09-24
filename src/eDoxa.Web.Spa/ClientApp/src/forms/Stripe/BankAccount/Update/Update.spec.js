import React from "react";
import Update from "./Update";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const tree = renderer
    .create(
      <Provider store={{ getState: () => {}, dispatch: action => {}, subscribe: () => {} }}>
        <Update />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
