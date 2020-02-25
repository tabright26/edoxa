import React from "react";
import renderer from "react-test-renderer";
import Reset from ".";
import { MemoryRouter } from "react-router-dom";
import { Provider } from "react-redux";
import store from "store";

it("renders correctly", () => {
  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <MemoryRouter>
          <Reset />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
