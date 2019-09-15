import React from "react";
import { Label, FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import { DELETE_ADDRESS_FORM } from "forms";

const DeleteAddressForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit} className="mt-3">
    <Label>Are you sure you want to remove this address?</Label>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: DELETE_ADDRESS_FORM })(DeleteAddressForm);
