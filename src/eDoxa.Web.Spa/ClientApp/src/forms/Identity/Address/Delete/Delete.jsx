import React from "react";
import { reduxForm } from "redux-form";
import { FormGroup, Form } from "reactstrap";

import Button from "../../../../components/Button";

import { DELETE_ADDRESS_FORM } from "../forms";

const DeleteAddressForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <FormGroup className="mb-0">
      <Button.Yes className="mr-2" />
      <Button.No onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: DELETE_ADDRESS_FORM })(DeleteAddressForm);
