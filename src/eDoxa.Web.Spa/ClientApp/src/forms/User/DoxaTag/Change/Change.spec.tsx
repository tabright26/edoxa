import React from "react";
import Change from "./Change";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";

it("renders correctly", () => {
  const store: any = {
    getState: () => {},
    dispatch: action => {},
    subscribe: () => {}
  };
  const tree = renderer
    .create(
      <Provider store={store}>
        <Change />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
