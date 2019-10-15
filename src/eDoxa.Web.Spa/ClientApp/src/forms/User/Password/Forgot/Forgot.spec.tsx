import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Forgot from "./Forgot";

it("renders without crashing", () => {
  const store: any = {
    getState: () => {},
    dispatch: action => {},
    subscribe: () => {}
  };
  const tree = renderer
    .create(
      <Provider store={store}>
        <Forgot />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
