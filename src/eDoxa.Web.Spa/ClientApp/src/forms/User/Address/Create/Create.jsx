import React from "react";
import { Field, reduxForm } from "redux-form";
import { FormGroup, Col, Form } from "reactstrap";
import validate from "./validate";
import Button from "../../../../components/Override/Button";
import Input from "../../../../components/Override/Input";
import { CREATE_ADDRESS_FORM } from "../../../../forms";

const CreateAddressForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Field type="text" name="country" label="Country" formGroup={FormGroup} component={Input.Text} />
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

export default reduxForm({ form: CREATE_ADDRESS_FORM, validate })(CreateAddressForm);
