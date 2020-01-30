import React, { FunctionComponent } from "react";
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
import UserAddressField from "components/User/Address/Field";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faFlag, faBirthdayCake } from "@fortawesome/free-solid-svg-icons";
import { RootState } from "store/types";
import { connect, MapStateToProps } from "react-redux";
import InputMask from "react-input-mask";
import { getTermsOfServicesPath } from "utils/coreui/constants";
import { Link } from "react-router-dom";

type StateProps = {};

type OwnProps = {};

export interface RegisterUserAccountFormData {
  email: string;
  password: string;
  countryIsoCode: string;
  dob: string;
}

type InnerProps = StateProps &
  InjectedFormProps<RegisterUserAccountFormData, Props>;

type OutterProps = OwnProps;

type Props = InnerProps & OutterProps;

const Register: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  submitting,
  anyTouched
}) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary anyTouched={anyTouched} error={error} />
    {/* <InputGroup className="mb-3">
      <InputGroupAddon addonType="prepend">
        <InputGroupText>
          <i className="icon-user"></i>
        </InputGroupText>
      </InputGroupAddon>
      <Field
        type="text"
        name="username"
        placeholder="Username"
        //autoComplete="username"
        component={Input.Text}
      />
    </InputGroup> */}
    <InputGroup className="mb-3">
      <InputGroupAddon addonType="prepend">
        <InputGroupText>@</InputGroupText>
      </InputGroupAddon>
      <Field
        type="text"
        name="email"
        placeholder="Email"
        size={null}
        //autoComplete="email"
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
            //autoComplete="password"
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
            placeholder="Repeat password"
            size={null}
            //autoComplete="new-password"
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
          <UserAddressField.CountryIsoCode size={null} placeholder="Country" />
        </InputGroup>
      </Col>
    </FormGroup>
    <p className="mb-3">
      By clicking this button, you agree to the{" "}
      <Link to={getTermsOfServicesPath()}>terms of services</Link>.
    </p>
    <FormGroup className="mb-0">
      <Button.Submit loading={submitting} block>
        Create Account
      </Button.Submit>
    </FormGroup>
  </Form>
);

const mapStateToProps: MapStateToProps<
  StateProps,
  OwnProps,
  RootState
> = () => {
  return {
    initialValues: {
      countryIsoCode: "CA"
    }
  };
};

const enhance = compose<InnerProps, OutterProps>(
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
    onSubmitSuccess: (_result, dispatch) => {
      dispatch(push("/authentication/login"));
    },
    validate: () => {
      const errors: FormErrors<RegisterUserAccountFormData> = {};
      //   for (let [key, value] of Object.entries(validatorOptions)) {
      //     if (!fieldsOptions[key].excluded) {
      //       errors[key] = getFieldValidationRuleMessage(value, values[key]);
      //     }
      //   }
      return errors;
    }
  })
);

export default enhance(Register);
