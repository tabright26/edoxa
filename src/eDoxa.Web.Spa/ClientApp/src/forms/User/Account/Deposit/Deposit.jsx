import React from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import { DEPOSIT_FORM } from "forms";
import validate from "./validate";
import Amounts from "../Amounts";

const DepositForm = ({ initialValues: { amounts }, handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Amounts amounts={amounts} />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: DEPOSIT_FORM, validate })(DepositForm);