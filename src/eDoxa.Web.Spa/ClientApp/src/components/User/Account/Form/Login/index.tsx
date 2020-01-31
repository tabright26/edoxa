import React, { FunctionComponent } from "react";
import {
  Form,
  InputGroup,
  InputGroupAddon,
  InputGroupText,
  Row,
  Col
} from "reactstrap";
import { Field, reduxForm, InjectedFormProps, FormErrors } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { LOGIN_USER_ACCOUNT_FORM } from "utils/form/constants";
import { compose } from "recompose";
import { ValidationSummary } from "components/Shared/ValidationSummary";
import { throwSubmissionError } from "utils/form/types";
import { loginUserAccount } from "store/actions/identity";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import { MapStateToProps, connect } from "react-redux";
import { RootState } from "store/types";
import queryString from "query-string";
import { withRouter, RouteComponentProps } from "react-router-dom";
import { LinkContainer } from "react-router-bootstrap";
import { getPasswordForgotPath } from "utils/coreui/constants";

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

const Login: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  anyTouched
}) => (
  <Form onSubmit={handleSubmit}>
    <ValidationSummary anyTouched={anyTouched} error={error} />
    <Field type="hidden" name="returnUrl" component={Input.Text} />
    <InputGroup className="mb-3">
      <InputGroupAddon addonType="prepend">
        <InputGroupText>
          <i className="icon-user"></i>
        </InputGroupText>
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
    <InputGroup className="mb-4">
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
        //autoComplete="current-password"
        component={Input.Password}
      />
    </InputGroup>
    <Row>
      <Col xs="6">
        <Button.Submit className="px-4">Login</Button.Submit>
      </Col>
      <Col xs="6" className="text-right">
        <LinkContainer to={getPasswordForgotPath()}>
          <Button.Link className="px-0" size="sm">
            Forgot password?
          </Button.Link>
        </LinkContainer>
      </Col>
    </Row>
  </Form>
);

const mapStateToProps: MapStateToProps<StateProps, Props, RootState> = (
  _state,
  ownProps
) => {
  const { returnUrl } = queryString.parse(ownProps.location.search);
  return {
    initialValues: { returnUrl }
  };
};

const enhance = compose<InnerProps, OutterProps>(
  withRouter,
  connect(mapStateToProps),
  reduxForm<FormData, Props>({
    form: LOGIN_USER_ACCOUNT_FORM,
    onSubmit: async (values, dispatch) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(loginUserAccount(values, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: result => {
      window.location = result.data;
    }
  })
);

export default enhance(Login);
