import React, { FunctionComponent } from "react";
import { FormGroup, Form, InputGroup, InputGroupAddon, InputGroupText } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { RESET_USER_PASSWORD_FORM } from "forms";
import { validate } from "./validate";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";

const ResetUserPasswordForm: FunctionComponent<any> = ({ handleSubmit, resetUserPassword, error }) => (
  <Form onSubmit={handleSubmit(data => resetUserPassword(data))}>
    {error && <FormValidation error={error} />}
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

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => any }, string>({ form: RESET_USER_PASSWORD_FORM, validate }));

export default enhance(ResetUserPasswordForm);