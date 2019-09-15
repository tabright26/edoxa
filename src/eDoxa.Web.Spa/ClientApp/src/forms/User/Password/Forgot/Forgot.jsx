import React from "react";
import { FormGroup, Form } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import { FORGOT_PASSWORD_FORM } from "forms";
import validate from "./validate";

const ForgotPasswordForm = ({ handleSubmit }) => (
  <Form onSubmit={handleSubmit}>
    <Field type="text" name="email" label="Email" formGroup={FormGroup} component={Input.Text} />
    <FormGroup className="mb-0">
      <Button.Submit block>Send Email</Button.Submit>
    </FormGroup>
  </Form>
);

export default reduxForm({ form: FORGOT_PASSWORD_FORM, validate })(ForgotPasswordForm);
