import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { reduxForm, Field } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { UPDATE_USER_DOXATAG_FORM } from "forms";
import { validate } from "./validate";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { updateUserDoxatag } from "store/root/user/doxatagHistory/actions";
import { throwSubmissionError } from "utils/form/types";

async function submit(values, dispatch) {
  try {
    return await new Promise((resolve, reject) => {
      const meta: any = { resolve, reject };
      dispatch(updateUserDoxatag(values, meta));
    });
  } catch (error) {
    throwSubmissionError(error);
  }
}

const UpdateUserDoxatagForm: FunctionComponent<any> = ({
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
      name="name"
      label="Name"
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
    form: UPDATE_USER_DOXATAG_FORM,
    validate
  })
);

export default enhance(UpdateUserDoxatagForm);
