import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import Input from "components/Shared/Input";
import Button from "components/Shared/Button";
import { UPDATE_USER_PHONE_FORM } from "forms";
import { compose } from "recompose";
import { validate } from "./validate";
import FormValidation from "components/Shared/Form/Validation";

const UpdateUserPhoneForm: FunctionComponent<any> = ({ updateUserPhone, handleSubmit, handleCancel, error }) => {
  return (
    <Form
      onSubmit={handleSubmit(values =>
        updateUserPhone(values).then(() => {
          handleCancel();
        })
      )}
    >
      {error && <FormValidation error={error} />}
      <Field type="text" name="number" label="Phone Number" formGroup={FormGroup} component={Input.Text} />
      <FormGroup className="mb-0">
        <Button.Save className="mr-2" />
        <Button.Cancel onClick={handleCancel} />
      </FormGroup>
    </Form>
  );
};

const enhance = compose<any, any>(reduxForm<any, { handleCancel: () => any }, string>({ form: UPDATE_USER_PHONE_FORM, validate }));

export default enhance(UpdateUserPhoneForm);
