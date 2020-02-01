import React, { FunctionComponent } from "react";
import {
  FormGroup,
  Form,
  InputGroup,
  InputGroupAddon,
  InputGroupText
} from "reactstrap";
import { Field, reduxForm, InjectedFormProps, FormErrors } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { RESET_USER_PASSWORD_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";
import { throwSubmissionError } from "utils/form/types";
import { resetUserPassword } from "store/actions/identity";
import { EMAIL_REQUIRED, PASSWORD_REQUIRED } from "utils/form/validators";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import { MapStateToProps, connect } from "react-redux";
import { RootState } from "store/types";
import queryString, { ParseOptions } from "query-string";
import { withRouter, RouteComponentProps } from "react-router-dom";
import { push } from "connected-react-router";

interface StateProps {}

interface FormData {
  code: string;
  email: string;
  password: string;
  newPassword: string;
}

interface OutterProps {}

type InnerProps = InjectedFormProps<FormData, Props> &
  StateProps &
  RouteComponentProps;

type Props = InnerProps & OutterProps;

const CustomForm: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  submitting,
  anyTouched
}) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary anyTouched={anyTouched} error={error} />
    <Field type="hidden" name="code" component={Input.Text} />
    <InputGroup size="sm" className="mb-3">
      <InputGroupAddon addonType="prepend">
        <InputGroupText>@</InputGroupText>
      </InputGroupAddon>
      <Field
        type="text"
        name="email"
        placeholder="Email"
        autoComplete="email"
        component={Input.Text}
      />
    </InputGroup>
    <InputGroup size="sm" className="mb-3">
      <InputGroupAddon addonType="prepend">
        <InputGroupText>
          <i className="icon-lock"></i>
        </InputGroupText>
      </InputGroupAddon>
      <Field
        type="password"
        name="password"
        placeholder="New password"
        autoComplete="password"
        component={Input.Password}
      />
    </InputGroup>
    <InputGroup size="sm" className="mb-3">
      <InputGroupAddon addonType="prepend">
        <InputGroupText>
          <i className="icon-lock"></i>
        </InputGroupText>
      </InputGroupAddon>
      <Field
        type="password"
        name="newPassword"
        placeholder="Repeat new password"
        autoComplete="new-password"
        component={Input.Password}
      />
    </InputGroup>
    <FormGroup className="mb-0">
      <Button.Submit loading={submitting} block>
        Reset
      </Button.Submit>
    </FormGroup>
  </Form>
);

const mapStateToProps: MapStateToProps<StateProps, Props, RootState> = (
  _state,
  ownProps
) => {
  const options: ParseOptions = {
    decode: false
  };
  const { code } = queryString.parse(ownProps.location.search, options);
  return {
    initialValues: { code }
  };
};

const enhance = compose<InnerProps, OutterProps>(
  withRouter,
  connect(mapStateToProps),
  reduxForm<FormData, Props>({
    form: RESET_USER_PASSWORD_FORM,
    onSubmit: async (values, dispatch) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(resetUserPassword(values, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (_result, dispatch) => {
      dispatch(push("/authentication/login"));
    },
    validate: values => {
      const errors: FormErrors<FormData> = {};
      if (!values.email) {
        errors.email = EMAIL_REQUIRED;
      }
      if (!values.password) {
        errors.password = PASSWORD_REQUIRED;
      } else if (values.password !== values.newPassword) {
        errors._error = "Repeat password doesn't match password";
      }
      return errors;
    }
  })
);

export default enhance(CustomForm);
