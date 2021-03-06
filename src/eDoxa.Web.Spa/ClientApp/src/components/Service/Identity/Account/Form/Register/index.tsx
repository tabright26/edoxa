import React, { FunctionComponent, useEffect } from "react";
import {
  FormGroup,
  Form,
  InputGroup,
  InputGroupAddon,
  InputGroupText,
  Col
} from "reactstrap";
import { Field, reduxForm, InjectedFormProps, FormErrors } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { REGISTER_USER_ACCOUNT_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";
import { registerUserAccount } from "store/actions/identity";
import { throwSubmissionError } from "utils/form/types";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import { push } from "connected-react-router";
import AddressField from "components/Service/Identity/Address/Field";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faFlag, faBirthdayCake } from "@fortawesome/free-solid-svg-icons";
import { RootState } from "store/types";
import { connect, MapStateToProps } from "react-redux";
import InputMask from "react-input-mask";
import { getLegalTermsOfUsePath } from "utils/coreui/constants";
import { Link } from "react-router-dom";
import {
  EMAIL_REQUIRED,
  PASSWORD_REQUIRED,
  DOB_REQUIRED
} from "utils/form/validators";
import { COUNTRY_CA, Country } from "types/identity";
import {
  isRegisterVisited,
  setRegisterVisited,
  setWorkflowStep
} from "utils/cookies/constants";
import { withCookies, ReactCookieProps } from "react-cookie";
import { publishUserCreatedEvent } from "utils/ga";

export interface RegisterUserAccountFormData {
  email: string;
  password: string;
  newPassword: string;
  countryIsoCode: Country;
  dob: string;
}

type StateProps = {};

type OwnProps = {};

type InnerProps = StateProps &
  InjectedFormProps<RegisterUserAccountFormData, Props> &
  ReactCookieProps;

type OutterProps = OwnProps;

type Props = InnerProps & OutterProps;

const Register: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  submitting,
  anyTouched,
  cookies
}) => {
  useEffect(() => {
    if (!isRegisterVisited(cookies)) {
      setRegisterVisited(cookies);
    }
  }, [cookies]);
  return (
    <Form onSubmit={handleSubmit}>
      <ValidationSummary anyTouched={anyTouched} error={error} />
      <InputGroup className="mb-3">
        <InputGroupAddon addonType="prepend">
          <InputGroupText>@</InputGroupText>
        </InputGroupAddon>
        <Field
          type="text"
          name="email"
          placeholder="Email"
          size={null}
          autoComplete="email"
          component={Input.Text}
        />
      </InputGroup>
      <FormGroup row className="mb-3">
        <Col sm={6}>
          <InputGroup>
            <InputGroupAddon addonType="prepend">
              <InputGroupText>
                <i className="icon-lock"></i>
              </InputGroupText>
            </InputGroupAddon>
            <Field
              type="password"
              name="password"
              placeholder="Password"
              size={null}
              autoComplete="password"
              component={Input.Password}
            />
          </InputGroup>
        </Col>
        <Col sm={6}>
          <InputGroup>
            <InputGroupAddon addonType="prepend">
              <InputGroupText>
                <i className="icon-lock"></i>
              </InputGroupText>
            </InputGroupAddon>
            <Field
              type="password"
              name="newPassword"
              placeholder="Confirm password"
              size={null}
              autoComplete="new-password"
              component={Input.Password}
            />
          </InputGroup>
        </Col>
      </FormGroup>
      <hr className="border-secondary" />
      <FormGroup row className="mb-4">
        <Col sm={6}>
          <InputGroup>
            <InputGroupAddon addonType="prepend">
              <InputGroupText>
                <FontAwesomeIcon icon={faBirthdayCake} />
              </InputGroupText>
            </InputGroupAddon>
            <Field
              type="text"
              name="dob"
              component={Input.Text}
              placeholder="MM/DD/YYYY"
              tag={InputMask}
              mask="11/11/1111"
              maskChar={null}
              size={null}
              formatChars={{
                "1": "[0-9]",
                A: "[A-Z]"
              }}
            />
          </InputGroup>
        </Col>
        <Col sm={6}>
          <InputGroup>
            <InputGroupAddon addonType="prepend">
              <InputGroupText>
                <FontAwesomeIcon icon={faFlag} />
              </InputGroupText>
            </InputGroupAddon>
            <AddressField.Country size={null} placeholder="Country" />
          </InputGroup>
        </Col>
      </FormGroup>
      <p className="mb-3">
        By clicking this button, you agree to the{" "}
        <Link to={getLegalTermsOfUsePath()}>terms of use</Link>.
      </p>
      <FormGroup className="mb-0">
        <Button.Submit loading={submitting} block>
          Create Account
        </Button.Submit>
      </FormGroup>
    </Form>
  );
};

const mapStateToProps: MapStateToProps<
  StateProps,
  OwnProps,
  RootState
> = () => {
  return {
    initialValues: {
      countryIsoCode: COUNTRY_CA
    }
  };
};

const enhance = compose<InnerProps, OutterProps>(
  withCookies,
  connect(mapStateToProps),
  reduxForm<RegisterUserAccountFormData, Props>({
    form: REGISTER_USER_ACCOUNT_FORM,
    onSubmit: async (values, dispatch) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(registerUserAccount(values, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (_result, dispatch, { cookies }) => {
      setWorkflowStep(cookies, 0);
      publishUserCreatedEvent();
      dispatch(push("/authentication/login"));
    },
    validate: values => {
      const errors: FormErrors<RegisterUserAccountFormData> = {};
      if (!values.email) {
        errors.email = EMAIL_REQUIRED;
      }
      if (!values.password) {
        errors.password = PASSWORD_REQUIRED;
      } else if (values.password !== values.newPassword) {
        errors._error = "Confirm password doesn't match password";
      }
      if (!values.dob) {
        errors.dob = DOB_REQUIRED;
      }
      return errors;
    }
  })
);

export default enhance(Register);
