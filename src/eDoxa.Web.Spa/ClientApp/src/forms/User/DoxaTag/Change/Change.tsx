import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm, Field } from "redux-form";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import { CHANGE_DOXATAG_FORM } from "forms";
import validate from "./validate";

const ChangeDoxaTagForm: FunctionComponent<any> = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Field type="text" name="name" label="DoxaTag" formGroup={FormGroup} component={Input.Text} />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm<any, { handleCancel: () => any }, string>({ form: CHANGE_DOXATAG_FORM, validate })(ChangeDoxaTagForm);
