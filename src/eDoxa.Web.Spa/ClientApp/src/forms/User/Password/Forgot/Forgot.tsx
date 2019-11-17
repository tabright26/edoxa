import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { FORGOT_USER_PASSWORD_FORM } from "forms";
import { validate } from "./validate";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";

const ForgotUserPasswordForm: FunctionComponent<any> = ({ handleSubmit, forgotUserPassword, error }) => (
  <Form onSubmit={handleSubmit(data => forgotUserPassword(data))}>
    {error && <FormValidation error={error} />}
    <Field type="text" name="email" label="Email" formGroup={FormGroup} component={Input.Text} />
    <FormGroup className="mb-0">
      <Button.Submit block>Send Email</Button.Submit>
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => any }, string>({ form: FORGOT_USER_PASSWORD_FORM, validate }));

export default enhance(ForgotUserPasswordForm);
