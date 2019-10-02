import React from "react";
import { FormGroup, Form } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import Button from "./node_modules/components/Shared/Override/Button";
import Input from "./node_modules/components/Shared/Override/Input";
import { CREATE_CLAN_FORM } from "forms";
import validate from "./validate";

const CreateClan = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Field type="text" name="name" label="Name" formGroup={FormGroup} component={Input.Text} />
    <Field type="text" name="summary" label="Summary" formGroup={FormGroup} component={Input.Text} />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: CREATE_CLAN_FORM, validate })(CreateClan);
