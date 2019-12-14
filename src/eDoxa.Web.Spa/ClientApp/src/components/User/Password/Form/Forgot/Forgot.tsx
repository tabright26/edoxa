import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { Field, reduxForm, InjectedFormProps } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { FORGOT_USER_PASSWORD_FORM } from "forms";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { throwSubmissionError } from "utils/form/types";
import { forgotUserPassword } from "store/actions/identity/actions";
import { EMAIL_REQUIRED, EMAIL_INVALID, emailRegex } from "validation";
import { AxiosActionCreatorMeta } from "utils/axios/types";

interface Props {}

interface FormData {
  email: string;
}

const validate = values => {
  const errors: any = {};
  if (!values.email) {
    errors.email = EMAIL_REQUIRED;
  } else if (!emailRegex.test(values.email)) {
    errors.email = EMAIL_INVALID;
  }
  return errors;
};

async function submit(values, dispatch) {
  try {
    return await new Promise((resolve, reject) => {
      const meta: AxiosActionCreatorMeta = { resolve, reject };
      dispatch(forgotUserPassword(values, meta));
    });
  } catch (error) {
    throwSubmissionError(error);
  }
}

const ForgotUserPasswordForm: FunctionComponent<InjectedFormProps<FormData> &
  Props &
  any> = ({ handleSubmit, handleCancel, dispatch, error }) => (
  <Form
    onSubmit={handleSubmit(data =>
      submit(data, dispatch).then(() => handleCancel())
    )}
  >
    {error && <FormValidation error={error} />}
    <Field
      type="text"
      name="email"
      label="Email"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <FormGroup className="mb-0">
      <Button.Submit block>Send Email</Button.Submit>
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(
  reduxForm<FormData, Props>({
    form: FORGOT_USER_PASSWORD_FORM,
    validate
  })
);

export default enhance(ForgotUserPasswordForm);
