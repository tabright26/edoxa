import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import Input from "components/Shared/Override/Input";
import Button from "components/Shared/Override/Button";
import { UPDATE_USER_PHONE_FORM } from "forms";
import { compose } from "recompose";
import { validate } from "./validate";

const UpdateUserPhoneForm: FunctionComponent<any> = ({ updateUserPhone, handleSubmit, handleCancel }) => {
  return (
    <Form onSubmit={handleSubmit(values => updateUserPhone(values).then(() => handleCancel()))}>
      <Field type="text" name="phoneNumber" label="Phone Number" formGroup={FormGroup} component={Input.Text} />
      <FormGroup className="mb-0">
        <Button.Save className="mr-2" />
        <Button.Cancel onClick={handleCancel} />
      </FormGroup>
    </Form>
  );
};

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => any }, string>({ form: UPDATE_USER_PHONE_FORM, validate }));

export default enhance(UpdateUserPhoneForm);
