import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Aside from ".";

it("renders without crashing", () => {
  //Arrange
  const store: any = {
    getState: () => {},
    dispatch: action => {},
    subscribe: () => {}
  };

  //Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <Aside />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
