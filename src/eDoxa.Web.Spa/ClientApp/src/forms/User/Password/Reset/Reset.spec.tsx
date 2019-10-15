import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Reset from "./Reset";

it("renders without crashing", () => {
  const store: any = {
    getState: () => {},
    dispatch: action => {},
    subscribe: () => {}
  };
  const tree = renderer
    .create(
      <Provider store={store}>
        <Reset />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
