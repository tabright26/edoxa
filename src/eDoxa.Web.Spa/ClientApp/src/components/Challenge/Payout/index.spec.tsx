import React from "react";
import renderer from "react-test-renderer";
import Payout from ".";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";

it("renders without crashing", () => {
  // Arrange
  const store: any = {
    getState: () => {},
    dispatch: action => {},
    subscribe: () => {}
  };

  //Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <MemoryRouter>
          <Payout challengeId="123" />
        </MemoryRouter>
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
