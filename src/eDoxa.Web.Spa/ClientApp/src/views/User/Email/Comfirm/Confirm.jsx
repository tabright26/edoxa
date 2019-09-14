import React, { useState, useEffect } from "react";
import { Redirect } from "react-router-dom";
import { Alert } from "reactstrap";
import withUserContainer from "../../../../containers/withUserContainer";
import queryString from "query-string";

const EmailConfirm = ({ location, actions }) => {
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

export default withUserContainer(EmailConfirm);
