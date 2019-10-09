import React from "react";
import Accept from "./Accept";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const tree = renderer
    .create(
      <Provider store={{ getState: () => {}, dispatch: action => {}, subscribe: () => {} }}>
        <Accept />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
