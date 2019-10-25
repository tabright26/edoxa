import React from "react";
import renderer from "react-test-renderer";
import { Form } from "redux-form";
import { Provider } from "react-redux";
import Country from "./Country";

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
        <Form>
          <Country />
        </Form>
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
