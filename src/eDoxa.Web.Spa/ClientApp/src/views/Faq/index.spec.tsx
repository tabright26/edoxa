import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Faq from ".";
import { MemoryRouter } from "react-router-dom";
import store from "store";

it("renders without crashing", () => {
  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <MemoryRouter>
          <Faq />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
