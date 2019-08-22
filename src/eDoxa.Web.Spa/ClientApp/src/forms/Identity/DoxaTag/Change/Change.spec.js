import React from "react";
import Change from "./Change";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const tree = renderer
    .create(
      <Provider store={{ getState: () => {}, dispatch: action => {}, subscribe: () => {} }}>
        <Change />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
