import React from "react";
import Create from "./Create";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const tree = renderer
    .create(
      <Provider store={{ getState: () => {}, dispatch: action => {}, subscribe: () => {} }}>
        <Create initialValues={{ userId: "" }} />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
