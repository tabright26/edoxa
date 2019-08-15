import React from "react";
import { reduxForm } from "redux-form";
import { FormGroup, Form } from "reactstrap";

import Button from "../../../../components/Buttons";

import { DELETE_ADDRESS_FORM } from "../forms";

const DeleteAddressForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: DELETE_ADDRESS_FORM })(DeleteAddressForm);
