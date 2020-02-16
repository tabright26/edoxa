import React from "react";
import renderer from "react-test-renderer";
import Confirm from "../Comfirm";
import { MemoryRouter } from "react-router-dom";
import { Provider } from "react-redux";
import store from "store";

it("renders correctly", () => {
  const tree = renderer
    .create(
      <Provider store={store}>
        <MemoryRouter>
          <Confirm />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
