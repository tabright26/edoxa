import React, { FunctionComponent } from "react";
import { FormGroup, Col, Form } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import FieldCountry from "components/Shared/Override/Field/Country";
import { CREATE_USER_ADDRESS_FORM } from "forms";
import { validate } from "./validate";
import { compose } from "recompose";

const CreateUserAddressForm: FunctionComponent<any> = ({ createUserAddress, handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit(data => createUserAddress(data).then(() => handleCancel()))}>
    <FormGroup>
      <FieldCountry />
    </FormGroup>
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

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => {} }, string>({ form: CREATE_USER_ADDRESS_FORM, validate }));

export default enhance(CreateUserAddressForm);
