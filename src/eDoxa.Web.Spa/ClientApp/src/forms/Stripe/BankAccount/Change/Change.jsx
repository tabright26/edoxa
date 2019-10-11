import React from "react";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import { Field, reduxForm } from "redux-form";
import { FormGroup, Form } from "reactstrap";
import { CREATE_BANK_ACCOUNT_FORM } from "forms";

const ChangeBankAccountForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Field type="text" name="country" label="country" formGroup={FormGroup} component={Input.Text} />
    <Field type="text" name="accountHolderName" label="accountHolderName" formGroup={FormGroup} component={Input.Text} />
    <Field type="text" name="routingNumber" label="routingNumber" formGroup={FormGroup} component={Input.Text} />
    <Field type="text" name="accountNumber" label="accountNumber" formGroup={FormGroup} component={Input.Text} />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: CREATE_BANK_ACCOUNT_FORM })(ChangeBankAccountForm);
