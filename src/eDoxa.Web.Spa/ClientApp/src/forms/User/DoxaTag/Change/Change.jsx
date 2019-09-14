import React from "react";
import { reduxForm, Field } from "redux-form";
import { FormGroup, Form } from "reactstrap";
import Button from "../../../../components/Override/Button";
import Input from "../../../../components/Override/Input";
import validate from "./validate";
import { CHANGE_DOXATAG_FORM } from "../../../../forms";

const ChangeDoxaTagForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Field type="text" name="name" label="DoxaTag" formGroup={FormGroup} component={Input.Text} />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: CHANGE_DOXATAG_FORM, validate })(ChangeDoxaTagForm);
