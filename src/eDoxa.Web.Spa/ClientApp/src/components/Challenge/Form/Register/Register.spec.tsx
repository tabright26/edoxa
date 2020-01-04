import React from "react";
import Register from "./Register";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";

it("renders correctly", () => {
  const store: any = {
    getState: () => {},
    dispatch: action => {},
    subscribe: () => {}
  };
  const tree = renderer
    .create(
      <Provider store={store}>
        <MemoryRouter>
          <Register userId="userId" />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
