import React from "react";
import { Card, CardBody } from "reactstrap";
import SecurityPassword from "forms/User/Password";
import withUserContainer from "containers/withUserContainer";

const ForgotPassword = ({ actions }) => (
  <Card className="mx-4">
    <CardBody className="p-4">
      <h1>Forgot Password</h1>
      <p className="text-muted">Forgot your account password</p>
      <SecurityPassword.Forgot onSubmit={fields => actions.forgotPassword(fields)} />
    </CardBody>
  </Card>
);

export default withUserContainer(ForgotPassword);
