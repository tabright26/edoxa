import React from "react";
import renderer from "react-test-renderer";
import List from ".";
import { Provider } from "react-redux";
import store from "store";

it("renders without crashing", () => {
  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <List />
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
