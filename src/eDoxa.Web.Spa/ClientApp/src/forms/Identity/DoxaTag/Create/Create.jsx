import React from "react";
import { Field, reduxForm } from "redux-form";
import { FormGroup, Input, Form } from "reactstrap";
import Button from "../../../../components/Button";
import { CREATE_DOXATAG_FORM } from "../forms";

const CreateDoxaTagForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <FormGroup>
      <Field type="text" name="name" component={({ input }) => <Input {...input} placeholder="DoxaTag" bsSize="sm" />} />
    </FormGroup>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: CREATE_DOXATAG_FORM })(CreateDoxaTagForm);
