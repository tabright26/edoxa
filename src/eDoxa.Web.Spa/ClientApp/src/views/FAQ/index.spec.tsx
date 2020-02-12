import React from "views/Faq/node_modules/react";
import renderer from "views/Faq/node_modules/react-test-renderer";
import { Provider } from "views/Faq/node_modules/react-redux";
import Faq from ".";

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
        <Faq />
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
