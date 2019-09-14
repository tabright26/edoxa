import React from "react";
import { Field, reduxForm } from "redux-form";
import { FormGroup, Col, Input, Form } from "reactstrap";
import myInput from "../../../../components/Shared/Override/Input";
import Button from "../../../../components/Shared/Override/Button";
import { DELETE_ADDRESS_FORM } from "../../../../forms";
import validate from "./validate";

const UpdateAddressForm = ({ country, handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <FormGroup>
      <Input type="text" value={country} bsSize="sm" disabled />
    </FormGroup>
    <FormGroup>
      <Field type="text" name="line1" label="Address line 1" component={props => <myInput.Text {...props} />} />
    </FormGroup>
    <FormGroup>
      <Field type="text" name="line2" label="Address line 2 (optional)" component={props => <myInput.Text {...props} />} />
    </FormGroup>
    <FormGroup>
      <Field type="text" name="city" label="City" component={props => <myInput.Text {...props} />} />
    </FormGroup>
    <FormGroup row className="my-0">
      <Col xs="8">
        <FormGroup>
          <Field type="text" name="state" label="State" component={props => <myInput.Text {...props} />} />
        </FormGroup>
      </Col>
      <Col xs="4">
        <FormGroup>
          <Field type="text" name="postalCode" label="Postal Code" component={props => <myInput.Text {...props} />} />
        </FormGroup>
      </Col>
    </FormGroup>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: DELETE_ADDRESS_FORM, validate })(UpdateAddressForm);
