import React from "react";
import LeaveClan from "./LeaveClan";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const tree = renderer
    .create(
      <Provider store={{ getState: () => {}, dispatch: action => {}, subscribe: () => {} }}>
        <LeaveClan initialValues={{ clanId: "" }} />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
