import React, { FunctionComponent } from "react";
import { FormGroup, Form } from "reactstrap";
import { Field, reduxForm, InjectedFormProps, FormErrors } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { FORGOT_USER_PASSWORD_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";
import { throwSubmissionError } from "utils/form/types";
import { forgotUserPassword } from "store/actions/identity";
import { EMAIL_REQUIRED } from "utils/form/validators";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import { push } from "connected-react-router";
import { toastr } from "react-redux-toastr";
import { LinkContainer } from "react-router-bootstrap";
import { getDefaultPath } from "utils/coreui/constants";

type FormData = {
  email: string;
}

type OutterProps = {}

type InnerProps = InjectedFormProps<FormData, Props>;

type Props = InnerProps & OutterProps;

const Forgot: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  submitting,
  anyTouched
}) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary anyTouched={anyTouched} error={error} />
    <Field
      type="text"
      name="email"
      placeholder="Email"
      autoComplete="email"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <FormGroup className="mb-0 d-flex">
      <Button.Submit loading={submitting} className="w-25">
        Send
      </Button.Submit>
      <LinkContainer to="/authentication/login">
        <Button.Link className="p-0 ml-auto my-auto" size="lg">
          Return to login page
        </Button.Link>
      </LinkContainer>
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
    onSubmitSuccess: (_result, dispatch) => {
      dispatch(push(getDefaultPath()));
      setTimeout(function() {
        toastr.success(
          "Email sent",
          "We have sent you a link to reset your password by email."
        );
      }, 1500);
    },
    validate: values => {
      const errors: FormErrors<FormData> = {};
      if (!values.email) {
        errors.email = EMAIL_REQUIRED;
      }
      return errors;
    }
  })
);

export default enhance(Forgot);
