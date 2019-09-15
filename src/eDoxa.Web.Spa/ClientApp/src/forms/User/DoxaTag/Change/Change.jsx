import React from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm, Field } from "redux-form";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import { CHANGE_DOXATAG_FORM } from "forms";
import validate from "./validate";

const ChangeDoxaTagForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <FormGroup>
      <Field type="text" name="name" label="DoxaTag" component={props => <Input.Text {...props} />} />
    </FormGroup>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: CHANGE_DOXATAG_FORM, validate })(ChangeDoxaTagForm);
