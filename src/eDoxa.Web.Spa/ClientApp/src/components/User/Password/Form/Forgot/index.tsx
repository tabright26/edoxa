import React, { FunctionComponent } from "react";
import { FormGroup, Form, Button as BsButton } from "reactstrap";
import { Field, reduxForm, InjectedFormProps, FormErrors } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { FORGOT_USER_PASSWORD_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";
import { throwSubmissionError } from "utils/form/types";
import { forgotUserPassword } from "store/actions/identity";
import {
  EMAIL_REQUIRED,
  EMAIL_INVALID,
  EMAIL_REGEXP
} from "utils/form/validators";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import { push } from "connected-react-router";
import { toastr } from "react-redux-toastr";
import { LinkContainer } from "react-router-bootstrap";

interface FormData {
  email: string;
}

interface OutterProps {}

type InnerProps = InjectedFormProps<FormData, Props>;

type Props = InnerProps & OutterProps;

const CustomForm: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  submitting
}) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary error={error} />
    <Field
      type="text"
      name="email"
      placeholder="Email"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <FormGroup className="mb-0 d-flex">
      <Button.Submit loading={submitting} className="w-25">
        Send
      </Button.Submit>
      <LinkContainer to="/authentication/login">
        <BsButton className="ml-auto my-auto" color="link" size="sm">
          Return to login page
        </BsButton>
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
      dispatch(push("/"));
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
      } else if (!EMAIL_REGEXP.test(values.email)) {
        errors.email = EMAIL_INVALID;
      }
      return errors;
    }
  })
);

export default enhance(CustomForm);
