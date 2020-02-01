import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Home from ".";

it("renders without crashing", () => {
  // Arrange
  const store: any = {
    getState: () => {},
    dispatch: action => {},
    subscribe: () => {}
  };

  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <Home />
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
