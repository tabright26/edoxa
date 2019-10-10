import React from "react";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import { Field, reduxForm } from "redux-form";
import { FormGroup, Form } from "reactstrap";
import { CREATE_BANK_ACCOUNT_FORM } from "forms";

const style = {
  backgroundColor: "#fff",
  color: "#000",
  borderColor: "#f1f1f1"
};

const ChangeBankAccountForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Field style={style} type="text" name="country" label="Country" formGroup={FormGroup} component={Input.Text} />
    <Field style={style} type="text" name="accountHolderName" label="Account holder name" formGroup={FormGroup} component={Input.Text} />
    <Field style={style} type="text" name="routingNumber" label="Routing number" formGroup={FormGroup} component={Input.Text} />
    <Field style={style} type="text" name="accountNumber" label="Account number" formGroup={FormGroup} component={Input.Text} />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: CREATE_BANK_ACCOUNT_FORM })(ChangeBankAccountForm);
