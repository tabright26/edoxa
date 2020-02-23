import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Challenges from ".";
import { MemoryRouter } from "react-router-dom";
import store from "store";

it("renders without crashing", () => {
  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <MemoryRouter>
          <Challenges />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
