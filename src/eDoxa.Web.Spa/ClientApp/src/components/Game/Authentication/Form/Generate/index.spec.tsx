import React from "react";
import Generate from ".";
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
        <Generate />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
