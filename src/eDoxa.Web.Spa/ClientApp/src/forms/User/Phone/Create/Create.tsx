import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import Input from "components/Shared/Override/Input";
import Button from "components/Shared/Override/Button";
import { CREATE_PHONE_FORM } from "forms";
import { compose } from "recompose";
import { validate } from "./validate";

const CreatePhoneForm: FunctionComponent<any> = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Field type="text" name="phoneNumber" label="Phone Number" formGroup={FormGroup} component={Input.Text} />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => any }, string>({ form: CREATE_PHONE_FORM, validate }));

export default enhance(CreatePhoneForm);
