import React from "react";
import { Field, reduxForm } from "redux-form";
import { FormGroup, Col, Input, Form } from "reactstrap";
import Button from "../../../../components/Buttons";
import { CREATE_PERSONALINFO_FORM } from "../forms";

const CreatePersonalInfoForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <FormGroup>
      <Field type="text" name="country" component={({ input }) => <Input {...input} placeholder="Country" bsSize="sm" />} />
    </FormGroup>
    <FormGroup>
      <Field type="text" name="line1" component={({ input }) => <Input {...input} placeholder="Address line 1" bsSize="sm" />} />
    </FormGroup>
    <FormGroup>
      <Field type="text" name="line2" component={({ input }) => <Input {...input} placeholder="Address line 2 (optional)" bsSize="sm" />} />
    </FormGroup>
    <FormGroup>
      <Field type="text" name="city" component={({ input }) => <Input {...input} placeholder="City" bsSize="sm" />} />
    </FormGroup>
    <FormGroup row className="my-0">
      <Col xs="8">
        <FormGroup>
          <Field type="text" name="state" component={({ input }) => <Input {...input} placeholder="State" bsSize="sm" />} />
        </FormGroup>
      </Col>
      <Col xs="4">
        <FormGroup>
          <Field type="text" name="postalCode" component={({ input }) => <Input {...input} placeholder="Postal Code" bsSize="sm" />} />
        </FormGroup>
      </Col>
    </FormGroup>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: CREATE_PERSONALINFO_FORM })(CreatePersonalInfoForm);
