import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Reset from "./Reset";

it("renders without crashing", () => {
    const tree = renderer
    .create(
      <Provider store={{ getState: () => {}, dispatch: action => {}, subscribe: () => {} }}>
        <Reset />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
