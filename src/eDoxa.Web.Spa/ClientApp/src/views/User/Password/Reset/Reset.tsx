import React, { FunctionComponent } from "react";
import { Redirect } from "react-router-dom";
import { Card, CardBody } from "reactstrap";
import queryString from "query-string";
import PasswordForm from "forms/User/Password";

const ResetPassword: FunctionComponent<any> = ({ location }) => {
  const code = queryString.parse(location.search).code;
  if (!code) {
    return <Redirect to="/errors/404" />;
  }
  return (
    <Card className="mx-4">
      <CardBody className="p-4">
        <h1>Reset Password</h1>
        <p className="text-muted">Reset your account password</p>
        <PasswordForm.Reset code={code} />
      </CardBody>
    </Card>
  );
};

export default ResetPassword;
