import React from "react";
import { Form, Field, reduxForm } from "redux-form";
import { Col, FormGroup, Row } from "reactstrap";
import validate from "./validate";
import Button from "../../../../components/Override/Button";
import Input from "../../../../components/Override/Input";
import { UPDATE_BANK_FORM } from "../../../../forms";

//<Field type="text" name="ccName" label="Enter your name" component={props => <myInput.Text {...props} />} />

const UpdateStripeCreditCardForm = ({ bank, handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Row>
      <Col xs="4">
        <FormGroup>
          <Field type="text" name="name" label={bank.account_holder_name} component={props => <Input.Text {...props} />} />
        </FormGroup>
      </Col>
    </Row>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: UPDATE_BANK_FORM, validate })(UpdateStripeCreditCardForm);
