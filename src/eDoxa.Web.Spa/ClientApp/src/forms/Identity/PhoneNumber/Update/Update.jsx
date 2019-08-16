import React from "react";
import { Field, reduxForm } from "redux-form";
import { FormGroup, Input, Form } from "reactstrap";
import Button from "../../../../components/Button";
import { UPDATE_PHONENUMBER_FORM } from "../forms";

const UpdatePhoneNumberForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <FormGroup>
      <Field type="text" name="phoneNumber" component={({ input }) => <Input {...input} placeholder="Phone Number" bsSize="sm" />} />
    </FormGroup>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: UPDATE_PHONENUMBER_FORM })(UpdatePhoneNumberForm);
