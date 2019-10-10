import React from "react";
import Decline from "./Decline";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const tree = renderer
    .create(
      <Provider store={{ getState: () => {}, dispatch: action => {}, subscribe: () => {} }}>
        <Decline />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
