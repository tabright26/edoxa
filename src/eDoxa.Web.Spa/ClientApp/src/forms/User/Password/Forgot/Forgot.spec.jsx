import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Forgot from "./Forgot";

it("renders without crashing", () => {
  const tree = renderer
    .create(
      <Provider store={{ getState: () => {}, dispatch: action => {}, subscribe: () => {} }}>
        <Forgot />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
