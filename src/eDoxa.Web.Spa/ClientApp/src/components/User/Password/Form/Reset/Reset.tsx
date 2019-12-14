import React, { FunctionComponent } from "react";
import {
  FormGroup,
  Form,
  InputGroup,
  InputGroupAddon,
  InputGroupText
} from "reactstrap";
import { Field, reduxForm, InjectedFormProps } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { RESET_USER_PASSWORD_FORM } from "forms";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { throwSubmissionError } from "utils/form/types";
import { resetUserPassword } from "store/actions/identity/actions";
import {
  emailRegex,
  passwordRegex,
  EMAIL_REQUIRED,
  EMAIL_INVALID,
  PASSWORD_REQUIRED,
  PASSWORD_INVALID
} from "validation";
import { AxiosActionCreatorMeta } from "utils/axios/types";

interface Props {}

interface FormData {}

const validate = values => {
  const errors: any = {};
  if (!values.email) {
    errors.email = EMAIL_REQUIRED;
  } else if (!emailRegex.test(values.email)) {
    errors.email = EMAIL_INVALID;
  }
  if (!values.password) {
    errors.password = PASSWORD_REQUIRED;
  } else if (!passwordRegex.test(values.password)) {
    errors.password = PASSWORD_INVALID;
  }
  return errors;
};

async function submit(values, dispatch) {
  try {
    return await new Promise((resolve, reject) => {
      const meta: AxiosActionCreatorMeta = { resolve, reject };
      dispatch(resetUserPassword(values, meta));
    });
  } catch (error) {
    throwSubmissionError(error);
  }
}

const ResetUserPasswordForm: FunctionComponent<InjectedFormProps<FormData> &
  Props &
  any> = ({ handleSubmit, handleCancel, dispatch, error }) => (
  <Form
    onSubmit={handleSubmit(data =>
      submit(data, dispatch).then(() => handleCancel())
    )}
  >
    {error && <FormValidation error={error} />}
    <InputGroup className="mb-3">
      <InputGroupAddon addonType="prepend">
        <InputGroupText>@</InputGroupText>
      </InputGroupAddon>
      <Field type="text" name="email" label="Email" component={Input.Text} />
    </InputGroup>
    <InputGroup className="mb-3">
      <InputGroupAddon addonType="prepend">
        <InputGroupText>
          <i className="icon-lock"></i>
        </InputGroupText>
      </InputGroupAddon>
      <Field
        type="password"
        name="password"
        label="Password"
        component={Input.Password}
      />
    </InputGroup>
    <InputGroup className="mb-3">
      <InputGroupAddon addonType="prepend">
        <InputGroupText>
          <i className="icon-lock"></i>
        </InputGroupText>
      </InputGroupAddon>
      <Field
        type="password"
        name="confirmPassword"
        label="Confirm Password"
        component={Input.Password}
      />
    </InputGroup>
    <FormGroup className="mb-0">
      <Button.Submit block>Reset</Button.Submit>
    </FormGroup>
  </Form>
);

const enhance = compose<any, any>(
  reduxForm<FormData, Props>({
    form: RESET_USER_PASSWORD_FORM,
    validate
  })
);

export default enhance(ResetUserPasswordForm);
