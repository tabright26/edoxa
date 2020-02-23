import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Home from ".";
import store from "store";
import { MemoryRouter } from "react-router-dom";

it("renders without crashing", () => {
  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <MemoryRouter>
          <Home />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
