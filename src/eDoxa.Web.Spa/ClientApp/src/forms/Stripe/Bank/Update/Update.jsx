import React from "react";
import { Form, Field, reduxForm } from "redux-form";
import { Col, FormGroup, Row } from "reactstrap";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import { UPDATE_BANK_FORM } from "forms";
import validate from "./validate";

const UpdateStripeBankForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Row>
      <Col xs="4">
        <Field type="text" name="account_holder_name" formGroup={FormGroup} component={Input.Text} />
      </Col>
    </Row>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: UPDATE_BANK_FORM, validate })(UpdateStripeBankForm);
