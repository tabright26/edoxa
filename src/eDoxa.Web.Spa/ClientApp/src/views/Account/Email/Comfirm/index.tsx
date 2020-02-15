import React, { useState, useEffect, FunctionComponent } from "react";
import { Redirect, RouteComponentProps, withRouter } from "react-router-dom";
import { connect, MapDispatchToProps } from "react-redux";
import { Alert } from "reactstrap";
import queryString, { ParseOptions } from "query-string";
import { compose } from "recompose";
import { confirmUserEmail } from "store/actions/identity";
import { getError404Path } from "utils/coreui/constants";
import authorizeService from "utils/oidc/AuthorizeService";
import {
  CONFIRM_USER_EMAIL_SUCCESS,
  ConfirmUserEmailAction
} from "store/actions/identity/types";

type DispatchProps = {
  confirmUserEmail: (userId: string | any, code: string | any) => void;
};

type OwnProps = RouteComponentProps;

type InnerProps = DispatchProps & OwnProps;

type OutterProps = {};

type Props = InnerProps & OutterProps;

const EmailConfirm: FunctionComponent<Props> = ({
  location,
  confirmUserEmail
}) => {
  const [notFound, setNotFound] = useState(false);
  useEffect(() => {
    const options: ParseOptions = {
      decode: false
    };
    const { userId, code } = queryString.parse(location.search, options);
    if (!userId || !code) {
      setNotFound(true);
    }
    if (!notFound) {
      confirmUserEmail(userId, code);
    }
  }, [confirmUserEmail, location.search, notFound]);
  if (notFound) {
    return <Redirect to={getError404Path()} />;
  }
  return (
    <Alert color="primary">
      <h4 className="alert-heading text-uppercase">Email confirmed</h4>
      <hr />
      <p className="mb-0">Thank you for confirming your email.</p>
    </Alert>
  );
};

const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
  dispatch: any
) => {
  return {
    confirmUserEmail: (userId: string, code: string) => {
      dispatch(confirmUserEmail(userId, code)).then(
        (action: ConfirmUserEmailAction) => {
          if (action.type === CONFIRM_USER_EMAIL_SUCCESS) {
            authorizeService.signIn({
              returnUrl: window.location.pathname
            });
          }
        }
      );
    }
  };
};

const enhance = compose<InnerProps, OutterProps>(
  withRouter,
  connect(null, mapDispatchToProps)
);

export default enhance(EmailConfirm);
