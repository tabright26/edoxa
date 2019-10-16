import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm, Field } from "redux-form";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import { UPDATE_USER_DOXATAG_FORM } from "forms";
import { validate } from "./validate";
import { compose } from "recompose";

const UpdateUserDoxatagForm: FunctionComponent<any> = ({ updateUserDoxatag, handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit(data => updateUserDoxatag(data).then(() => handleCancel()))}>
    <Field type="text" name="name" label="Name" formGroup={FormGroup} component={Input.Text} />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => any }, string>({ form: UPDATE_USER_DOXATAG_FORM, validate }));

export default enhance(UpdateUserDoxatagForm);
