import React from "react";
import { Field, reduxForm } from "redux-form";
import { FormGroup, Form } from "reactstrap";
import Input from "../../../../components/Override/Input";
import Button from "../../../../components/Override/Button";
import { UPDATE_PHONENUMBER_FORM } from "../../../../forms";

const UpdatePhoneNumberForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Field type="text" name="phoneNumber" label="Phone Number" formGroup={FormGroup} component={Input.Text} />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: UPDATE_PHONENUMBER_FORM })(UpdatePhoneNumberForm);
