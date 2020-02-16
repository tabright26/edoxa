import React from "react";
import renderer from "react-test-renderer";
import Timeline from ".";
import { MemoryRouter } from "react-router-dom";
import { Provider } from "react-redux";
import store from "store";

it("renders without crashing", () => {
  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <MemoryRouter>
          <Timeline />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
