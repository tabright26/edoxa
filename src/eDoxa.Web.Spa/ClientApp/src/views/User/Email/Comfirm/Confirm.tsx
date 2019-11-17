import React, { useState, useEffect, FunctionComponent } from "react";
import { Redirect } from "react-router-dom";
import { connect } from "react-redux";
import { Alert } from "reactstrap";
import { withUser } from "store/root/user/container";
import queryString from "query-string";
import { compose } from "recompose";
import { confirmUserEmail } from "store/root/user/email/actions";

const EmailConfirm: FunctionComponent<any> = ({ location, confirmUserEmail }) => {
  const [notFound, setNotFound] = useState(false);
  useEffect(() => {
    const { userId, code } = queryString.parse(location.search);
    if (!userId || !code) {
      setNotFound(true);
    }
    if (!notFound) {
      confirmUserEmail(userId, code);
    }
  }, [confirmUserEmail, location.search, notFound]);
  if (notFound) {
    return <Redirect to="/errors/404" />;
  }
  return (
    <Alert color="primary">
      <h4 className="alert-heading">Confirm Email</h4>
      <p className="mb-0">Tank you for confirming your email...</p>
    </Alert>
  );
};

const mapDispatchToProps = (dispatch: any) => {
  return {
    confirmUserEmail: (userId: string, code: string) => dispatch(confirmUserEmail(userId, code))
  };
};

const enhance = compose<any, any>(
  withUser,
  connect(
    null,
    mapDispatchToProps
  )
);

export default enhance(EmailConfirm);
