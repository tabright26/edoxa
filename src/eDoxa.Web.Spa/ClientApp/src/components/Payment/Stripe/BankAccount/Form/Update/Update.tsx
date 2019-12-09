import React, { FunctionComponent } from "react";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { Field, reduxForm } from "redux-form";
import { FormGroup, Form } from "reactstrap";
import { UPDATE_STRIPE_BANKACCOUNT_FORM } from "forms";
import { validate } from "./validate";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";

const UpdateStripeBankAccountForm: FunctionComponent<any> = ({ updateStripeBankAccount, handleSubmit, handleCancel, error }) => (
  <Form
    onSubmit={handleSubmit(data =>
      updateStripeBankAccount(data).then(() => {
        handleCancel();
      })
    )}
  >
    {error && <FormValidation error={error} />}
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

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => any }, string>({ form: UPDATE_STRIPE_BANKACCOUNT_FORM, validate }));

export default enhance(UpdateStripeBankAccountForm);
