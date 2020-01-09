import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { Field, reduxForm, InjectedFormProps, FormErrors } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { FORGOT_USER_PASSWORD_FORM } from "utils/form/constants";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { throwSubmissionError } from "utils/form/types";
import { forgotUserPassword } from "store/actions/identity";
import { EMAIL_REQUIRED, EMAIL_INVALID, emailRegex } from "validation";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import { push } from "connected-react-router";

interface FormData {
  email: string;
}

interface OutterProps {}

type InnerProps = InjectedFormProps<FormData, Props>;

type Props = InnerProps & OutterProps;

const CustomForm: FunctionComponent<Props> = ({ handleSubmit, error }) => (
  <Form onSubmit={handleSubmit}>
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

const enhance = compose<InnerProps, OutterProps>(
  reduxForm<FormData, Props>({
    form: FORGOT_USER_PASSWORD_FORM,
    onSubmit: async (values, dispatch) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(forgotUserPassword(values, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (_result, dispatch) => dispatch(push("/")),
    validate: values => {
      const errors: FormErrors<FormData> = {};
      if (!values.email) {
        errors.email = EMAIL_REQUIRED;
      } else if (!emailRegex.test(values.email)) {
        errors.email = EMAIL_INVALID;
      }
      return errors;
    }
  })
);

export default enhance(CustomForm);
