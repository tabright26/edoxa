import React from "react";
import { Field, reduxForm } from "redux-form";
import { FormGroup, Input, Form } from "reactstrap";
import Button from "components/Buttons";
import { CHANGE_EMAIL_FORM } from "forms";

const ChangeEmailForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <FormGroup>
      <Field type="text" name="email" component={({ input }) => <Input {...input} placeholder="Email" bsSize="sm" />} />
    </FormGroup>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: CHANGE_EMAIL_FORM })(ChangeEmailForm);
