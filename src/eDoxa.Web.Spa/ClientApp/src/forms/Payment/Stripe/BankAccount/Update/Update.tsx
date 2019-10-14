import React, { FunctionComponent } from "react";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import { Field, reduxForm } from "redux-form";
import { FormGroup, Form } from "reactstrap";
import { UPDATE_BANK_ACCOUNT_FORM } from "forms";
import { validate } from "./validate";
import { compose } from "recompose";

const UpdateStripeBankAccountForm: FunctionComponent<any> = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Field type="text" name="currency" label="Currency" formGroup={FormGroup} component={Input.Text} />
    <Field type="text" name="accountHolderName" label="Account Holder Name" formGroup={FormGroup} component={Input.Text} />
    <Field type="text" name="routingNumber" label="Routing Number" formGroup={FormGroup} component={Input.Text} />
    <Field type="text" name="accountNumber" label="Account Number" formGroup={FormGroup} component={Input.Text} />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => any }, string>({ form: UPDATE_BANK_ACCOUNT_FORM, validate }));

export default enhance(UpdateStripeBankAccountForm);
