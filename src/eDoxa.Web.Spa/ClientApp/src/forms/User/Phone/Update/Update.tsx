import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import Input from "components/Shared/Input";
import Button from "components/Shared/Button";
import { UPDATE_USER_PHONE_FORM } from "forms";
import { compose } from "recompose";
import { validate } from "./validate";
import FormValidation from "components/Shared/Form/Validation";
import { updateUserPhone } from "store/root/user/phone/actions";
import { throwSubmissionError } from "utils/form/types";

async function submit(values, dispatch) {
  try {
    return await new Promise((resolve, reject) => {
      const meta: any = { resolve, reject };
      dispatch(updateUserPhone(values, meta));
    });
  } catch (error) {
    throwSubmissionError(error);
  }
}

const UpdateUserPhoneForm: FunctionComponent<any> = ({
  handleSubmit,
  handleCancel,
  dispatch,
  error
}) => (
  <Form
    onSubmit={handleSubmit(data =>
      submit(data, dispatch).then(() => handleCancel())
    )}
  >
    {error && <FormValidation error={error} />}
    <Field
      type="text"
      name="number"
      label="Phone Number"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(
  reduxForm<any, { handleCancel: () => any }, string>({
    form: UPDATE_USER_PHONE_FORM,
    validate
  })
);

export default enhance(UpdateUserPhoneForm);
