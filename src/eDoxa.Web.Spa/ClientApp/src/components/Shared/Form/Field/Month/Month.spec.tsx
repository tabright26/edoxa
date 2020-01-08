import React from "react";
import renderer from "react-test-renderer";
import { Form, reduxForm } from "redux-form";
import { Provider } from "react-redux";
import Month from "./Month";

it("renders without crashing", () => {
  // Arrange
  const store: any = {
    getState: () => {},
    dispatch: action => {},
    subscribe: () => {}
  };

  const FormWrapper = () => (
    <Form>
      <Month />
    </Form>
  );

  const CustomForm = reduxForm({ form: "TEST_FORM" })(FormWrapper);

  // Act
  const tree = renderer
    .create(
      <Provider store={store}>
        <CustomForm />
      </Provider>
    )
    .toJSON();

  // Assert
  expect(tree).toMatchSnapshot();
});
