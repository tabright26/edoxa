import React from "react";
import { FormGroup, Form, InputGroup, InputGroupAddon, InputGroupText } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import { RESET_PASSWORD_FORM } from "forms";
import validate from "./validate";

const ResetPasswordForm = ({ handleSubmit }) => (
  <Form onSubmit={handleSubmit}>
    <InputGroup className="mb-3">
      <InputGroupAddon addonType="prepend">
        <InputGroupText>@</InputGroupText>
      </InputGroupAddon>
      <Field type="text" name="email" label="Email" component={Input.Text} />
    </InputGroup>
    <InputGroup className="mb-3">
      <InputGroupAddon addonType="prepend">
        <InputGroupText>
          <i className="icon-lock"></i>
        </InputGroupText>
      </InputGroupAddon>
      <Field type="password" name="password" label="Password" component={Input.Password} />
    </InputGroup>
    <InputGroup className="mb-3">
      <InputGroupAddon addonType="prepend">
        <InputGroupText>
          <i className="icon-lock"></i>
        </InputGroupText>
      </InputGroupAddon>
      <Field type="password" name="confirmPassword" label="Confirm Password" component={Input.Password} />
    </InputGroup>
    <FormGroup className="mb-0">
      <Button.Submit block>Reset</Button.Submit>
    </FormGroup>
  </Form>
);

export default reduxForm({ form: RESET_PASSWORD_FORM, validate })(ResetPasswordForm);