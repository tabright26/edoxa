import React from "react";
import { Field, reduxForm } from "redux-form";
import { FormGroup, Form } from "reactstrap";
import Input from "../../../../components/Override/Input";
import Button from "../../../../components/Override/Button";
import { CHANGE_EMAIL_FORM } from "../../../../forms";

const ChangeEmailForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Field type="text" name="email" label="Email" formGroup={FormGroup} component={Input.Text} />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: CHANGE_EMAIL_FORM })(ChangeEmailForm);
