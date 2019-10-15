import React from "react";
import Create from "./Create";
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
        <Create handleCancel={(): any => {}} />
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
