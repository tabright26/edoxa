import React, { FunctionComponent } from "react";
import { Field, reduxForm } from "redux-form";
import { FormGroup, Form } from "reactstrap";
import Input from "components/Shared/Override/Input";
import Button from "components/Shared/Override/Button";
import { CHANGE_EMAIL_FORM } from "forms";

const ChangeEmailForm: FunctionComponent<any> = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Field type="text" name="email" label="Email" formGroup={FormGroup} component={Input.Text} />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm<any, { handleCancel: () => {} }, string>({ form: CHANGE_EMAIL_FORM })(ChangeEmailForm);
