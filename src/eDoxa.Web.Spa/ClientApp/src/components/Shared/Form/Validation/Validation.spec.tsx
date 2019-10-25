import React from "react";
import renderer from "react-test-renderer";
import { Form } from "redux-form";
import { Provider } from "react-redux";
import Validation from "./Validation";

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
          <Validation error={{}} />
        </Form>
      </Provider>
    )
    .toJSON();

  //Assert
  expect(tree).toMatchSnapshot();
});
