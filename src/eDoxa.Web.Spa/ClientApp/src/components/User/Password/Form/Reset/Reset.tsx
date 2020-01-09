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
import FormValidation from "components/Shared/Form/Validation";
import { throwSubmissionError } from "utils/form/types";
import { resetUserPassword } from "store/actions/identity";
import {
  emailRegex,
  passwordRegex,
  EMAIL_REQUIRED,
  EMAIL_INVALID,
  PASSWORD_REQUIRED,
  PASSWORD_INVALID
} from "validation";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import { REACT_APP_AUTHORITY } from "keys";
import { MapStateToProps, connect } from "react-redux";
import { RootState } from "store/types";
import queryString, { ParseOptions } from "query-string";
import { withRouter, RouteComponentProps } from "react-router-dom";

interface StateProps {}

interface FormData {
  code: string;
  email: string;
  password: string;
}

interface OutterProps {}

type InnerProps = InjectedFormProps<FormData, Props> &
  StateProps &
  RouteComponentProps;

type Props = InnerProps & OutterProps;

const CustomForm: FunctionComponent<Props> = ({ handleSubmit, error }) => (
  <Form onSubmit={handleSubmit}>
    {error && <FormValidation error={error} />}
    <Field type="hidden" name="code" component={Input.Text} />
    <InputGroup size="sm" className="mb-3">
      <InputGroupAddon addonType="prepend">
        <InputGroupText>@</InputGroupText>
      </InputGroupAddon>
      <Field type="text" name="email" label="Email" component={Input.Text} />
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
        label="Password"
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
    onSubmitSuccess: () => {
      window.location.href = `${REACT_APP_AUTHORITY}/account/login`; // TODO: Should be router constants.
    },
    validate: values => {
      const errors: FormErrors<FormData> = {};
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
    }
  })
);

export default enhance(CustomForm);
