import React from "react";
import { Field, reduxForm } from "redux-form";
import { FormGroup, Col, Form } from "reactstrap";
import validate from "./validate";
import Button from "../../../../components/Button";
import Input from "../../../../components/Input";
import { CREATE_ADDRESS_FORM } from "../../../../forms";

const CreateAddressForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <FormGroup>
      <Field type="text" name="country" label="Country" component={props => <Input.Text {...props} />} />
    </FormGroup>
    <FormGroup>
      <Field type="text" name="line1" label="Address line 1" component={props => <Input.Text {...props} />} />
    </FormGroup>
    <FormGroup>
      <Field type="text" name="line2" label="Address line 2 (optional)" component={props => <Input.Text {...props} />} />
    </FormGroup>
    <FormGroup>
      <Field type="text" name="city" label="City" component={props => <Input.Text {...props} />} />
    </FormGroup>
    <FormGroup row className="my-0">
      <Col xs="8">
        <FormGroup>
          <Field type="text" name="state" label="State" component={props => <Input.Text {...props} />} />
        </FormGroup>
      </Col>
      <Col xs="4">
        <FormGroup>
          <Field type="text" name="postalCode" label="Postal Code" component={props => <Input.Text {...props} />} />
        </FormGroup>
      </Col>
    </FormGroup>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: CREATE_ADDRESS_FORM, validate })(CreateAddressForm);
