import React from "react";
import Register from ".";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import store from "store";

it("renders correctly", () => {
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
