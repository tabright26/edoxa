import React from "react";
import { reduxForm } from "redux-form";
import { Label, FormGroup, Form } from "reactstrap";

import Button from "../../../../components/Override/Button";

import { DELETE_CREDITCARD_FORM } from "../../../../forms";

const DeleteStripeCreditCardForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit} className="mt-3">
    <Label>Are you sure you want to remove this credit card?</Label>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: DELETE_CREDITCARD_FORM })(DeleteStripeCreditCardForm);
