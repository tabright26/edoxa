import React from "react";
import Validate from "./Validate";
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
        <Validate />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
