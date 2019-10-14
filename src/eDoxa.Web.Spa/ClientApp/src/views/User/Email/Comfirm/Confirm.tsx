import React, { useState, useEffect, FunctionComponent } from "react";
import { Redirect } from "react-router-dom";
import { Alert } from "reactstrap";
import { connectUser } from "store/root/user/container";
import queryString from "query-string";
import { compose } from "recompose";

const EmailConfirm: FunctionComponent<any> = ({ location, actions }) => {
  const [notFound, setNotFound] = useState(false);
  useEffect(() => {
    const { userId, code } = queryString.parse(location.search);
    if (!userId || !code) {
      setNotFound(true);
    }
    if (!notFound) {
      actions.confirmEmail(userId, code);
    }
  }, [actions, location.search, notFound]);
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

const enhance = compose<any, any>(connectUser);

export default enhance(EmailConfirm);
