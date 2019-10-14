import React, { FunctionComponent } from "react";
import { Redirect } from "react-router-dom";
import { Card, CardBody } from "reactstrap";
import queryString from "query-string";
import { withtUser } from "store/root/user/container";
import PasswordForm from "forms/User/Password";
import { compose } from "recompose";

const ResetPassword: FunctionComponent<any> = ({ location, actions }) => {
  const code = queryString.parse(location.search).code;
  if (!code) {
    return <Redirect to="/errors/404" />;
  }
  return (
    <Card className="mx-4">
      <CardBody className="p-4">
        <h1>Reset Password</h1>
        <p className="text-muted">Reset your account password</p>
        <PasswordForm.Reset onSubmit={fields => actions.resetPassword(fields, code)} />
      </CardBody>
    </Card>
  );
};

const enhance = compose<any, any>(withtUser);

export default enhance(ResetPassword);
