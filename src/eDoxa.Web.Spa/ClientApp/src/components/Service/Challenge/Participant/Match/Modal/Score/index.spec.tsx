import React from "react";
import Score from ".";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import store from "store";

it("renders correctly", () => {
  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <Score />
      </Provider>
    )
    .toJSON();

  // Arrange
  expect(tree).toMatchSnapshot();
});
