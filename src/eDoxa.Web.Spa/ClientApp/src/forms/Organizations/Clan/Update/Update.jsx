import React from "react";
import { FormGroup, Form } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import Input from "components/Shared/Override/Input";
import Button from "components/Shared/Override/Button";
import { UPDATE_CLAN_FORM } from "forms";
import validate from "./validate";

const UpdateClanForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Field type="text" name="summary" label="Summary" formGroup={FormGroup} component={Input.Text} />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: UPDATE_CLAN_FORM, validate })(UpdateClanForm);
