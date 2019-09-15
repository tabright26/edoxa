import React from "react";
import { Field, reduxForm } from "redux-form";
import { FormGroup, Col, Form } from "reactstrap";
import Input from "../../../../components/Override/Input";
import Button from "../../../../components/Override/Button";
import { DELETE_ADDRESS_FORM } from "../../../../forms";
import validate from "./validate";

const UpdateAddressForm = ({ initialValues: { country }, handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Input.Text type="text" value={country} bsSize="sm" formGroup={FormGroup} disabled />
    <Field type="text" name="line1" label="Address line 1" formGroup={FormGroup} component={Input.Text} />
    <Field type="text" name="line2" label="Address line 2 (optional)" formGroup={FormGroup} component={Input.Text} />
    <Field type="text" name="city" label="City" formGroup={FormGroup} component={Input.Text} />
    <FormGroup row className="my-0">
      <Col xs="8">
        <Field type="text" name="state" label="State" formGroup={FormGroup} component={Input.Text} />
      </Col>
      <Col xs="4">
        <Field type="text" name="postalCode" label="Postal Code" formGroup={FormGroup} component={Input.Text} />
      </Col>
    </FormGroup>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: DELETE_ADDRESS_FORM, validate })(UpdateAddressForm);
