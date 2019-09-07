import React from "react";
import { Field, reduxForm } from "redux-form";
import { FormGroup, Form } from "reactstrap";
import validate from "./validate";
import Button from "../../../../components/Override/Button";
import Input from "../../../../components/Override/Input";
import { FORGOT_PASSWORD_FORM } from "../../../../forms";

const ForgotPasswordForm = ({ handleSubmit }) => (
  <Form onSubmit={handleSubmit}>
    <Field
      type="text"
      name="email"
      label="Email"
      component={props => (
        <FormGroup>
          <Input.Text {...props} />
        </FormGroup>
      )}
    />
    <FormGroup className="mb-0">
      <Button.Submit block>Send Email</Button.Submit>
    </FormGroup>
  </Form>
);

export default reduxForm({ form: FORGOT_PASSWORD_FORM, validate })(ForgotPasswordForm);
