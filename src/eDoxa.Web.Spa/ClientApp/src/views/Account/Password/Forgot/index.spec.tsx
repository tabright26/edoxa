import React from "react";
import renderer from "react-test-renderer";
import Forgot from "../Forgot";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import store from "store";

it("renders correctly", () => {
  const tree = renderer
    .create(
      <Provider store={store}>
        <MemoryRouter>
          <Forgot />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
