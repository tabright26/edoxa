import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Logo from ".";
import store from "store";

it("renders without crashing", () => {
  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <Logo />
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
