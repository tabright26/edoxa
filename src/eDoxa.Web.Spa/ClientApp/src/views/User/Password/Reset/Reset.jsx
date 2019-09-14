import React from "react";
import { Card, CardBody } from "reactstrap";
import { Redirect } from "react-router-dom";
import SecurityPassword from "../../../../forms/User/Password";
import withUserContainer from "../../../../containers/withUserContainer";
import queryString from "query-string";

const ResetPassword = ({ location, actions }) => {
  const code = queryString.parse(location.search).code;
  if (!code) {
    return <Redirect to="/errors/404" />;
  }
  return (
    <Card className="mx-4">
      <CardBody className="p-4">
        <h1>Reset Password</h1>
        <p className="text-muted">Reset your account password</p>
        <SecurityPassword.Reset onSubmit={fields => actions.resetPassword(fields, code)} />
      </CardBody>
    </Card>
  );
};

export default withUserContainer(ResetPassword);
