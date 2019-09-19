import React from "react";
import { Label, FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import { DELETE_PAYMENTMETHOD_FORM } from "forms";

const DeletePaymentMethodForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Label className="mb-3">Are you sure you want to delete this payment method?</Label>
    <FormGroup className="mb-0">
      <Button.Yes className="mr-2" />
      <Button.No onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: DELETE_PAYMENTMETHOD_FORM })(DeletePaymentMethodForm);
