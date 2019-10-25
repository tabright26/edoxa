import React from "react";
import renderer from "react-test-renderer";
import { Provider } from "react-redux";
import Breadcrumb from "./Breadcrumb";

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
        <Breadcrumb />
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
