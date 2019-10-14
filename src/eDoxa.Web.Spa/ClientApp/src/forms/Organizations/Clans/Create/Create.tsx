import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import { CREATE_CLAN_FORM } from "forms";

const CreateClan: FunctionComponent<any> = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Field type="text" name="name" label="Name" formGroup={FormGroup} component={Input.Text} />
    <FormGroup>
      <Button.Save />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm<any, { handleCancel: () => any }, string>({ form: CREATE_CLAN_FORM })(CreateClan);
