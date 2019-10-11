import React from "react";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import { Field, FormSection, reduxForm } from "redux-form";
import { FormGroup, Form } from "reactstrap";
import { CREATE_BANK_ACCOUNT_FORM } from "forms";

const VerifyAccountForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Field type="text" name="firstName" label="First Name" formGroup={FormGroup} component={Input.Text} />
    <Field type="text" name="lastName" label="Last Name" formGroup={FormGroup} component={Input.Text} />
    <Field type="text" name="email" label="Email" formGroup={FormGroup} component={Input.Text} />
    <Field type="text" name="gender" label="Gender" formGroup={FormGroup} component={Input.Text} />
    <FormSection name="dob">
      <Field type="text" name="day" label="day" formGroup={FormGroup} component={Input.Text} />
      <Field type="text" name="month" label="month" formGroup={FormGroup} component={Input.Text} />
      <Field type="text" name="year" label="year" formGroup={FormGroup} component={Input.Text} />
    </FormSection>
    <FormSection name="address">
      <Field type="text" name="line1" label="Line1" formGroup={FormGroup} component={Input.Text} />
      <Field type="text" name="city" label="City" formGroup={FormGroup} component={Input.Text} />
      <Field type="text" name="state" label="State" formGroup={FormGroup} component={Input.Text} />
      <Field type="text" name="postalCode" label="Postal Code" formGroup={FormGroup} component={Input.Text} />
    </FormSection>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: CREATE_BANK_ACCOUNT_FORM })(VerifyAccountForm);
