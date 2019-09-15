import React from "react";
import { FormGroup, Input, Form } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import { CREATE_PHONENUMBER_FORM } from "forms";

const CreatePhoneNumberForm = ({ handleSubmit, handleCancel }) => (
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

export default reduxForm({ form: CREATE_PHONENUMBER_FORM })(CreatePhoneNumberForm);
