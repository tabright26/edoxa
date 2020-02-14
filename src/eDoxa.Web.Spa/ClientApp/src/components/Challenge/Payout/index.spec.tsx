import React from "react";
import renderer from "react-test-renderer";
import Payout from ".";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";
import store from "store";

it("renders without crashing", () => {
  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <MemoryRouter>
          <Payout challengeId="123" />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
