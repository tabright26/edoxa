import React from "react";
import { Field, reduxForm } from "redux-form";
import { FormGroup, Form, InputGroup, InputGroupAddon, InputGroupText } from "reactstrap";
import validate from "./validate";
import Button from "../../../../components/Shared/Override/Button";
import Input from "../../../../components/Shared/Override/Input";
import { RESET_PASSWORD_FORM } from "../../../../forms";

const ResetPasswordForm = ({ handleSubmit }) => (
  <Form onSubmit={handleSubmit}>
    <Field
      type="text"
      name="email"
      label="Email"
      component={props => (
        <InputGroup className="mb-3">
          <InputGroupAddon addonType="prepend">
            <InputGroupText>@</InputGroupText>
          </InputGroupAddon>
          <Input.Text {...props} />
        </InputGroup>
      )}
    />
    <Field
      type="password"
      name="password"
      label="Password"
      component={props => (
        <InputGroup className="mb-3">
          <InputGroupAddon addonType="prepend">
            <InputGroupText>
              <i className="icon-lock"></i>
            </InputGroupText>
          </InputGroupAddon>
          <Input.Password {...props} />
        </InputGroup>
      )}
    />
    <Field
      type="password"
      name="confirmPassword"
      label="Confirm Password"
      component={props => (
        <InputGroup className="mb-3">
          <InputGroupAddon addonType="prepend">
            <InputGroupText>
              <i className="icon-lock"></i>
            </InputGroupText>
          </InputGroupAddon>
          <Input.Password {...props} />
        </InputGroup>
      )}
    />
    <FormGroup className="mb-0">
      <Button.Submit block>Reset</Button.Submit>
    </FormGroup>
  </Form>
);

export default reduxForm({ form: RESET_PASSWORD_FORM, validate })(ResetPasswordForm);
