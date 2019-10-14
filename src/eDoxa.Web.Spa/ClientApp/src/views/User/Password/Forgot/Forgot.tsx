import React, { FunctionComponent } from "react";
import { Card, CardBody } from "reactstrap";
import { connectUser } from "store/root/user/container";
import PasswordForm from "forms/User/Password";
import { compose } from "recompose";

const ForgotPassword: FunctionComponent<any> = ({ actions }) => (
  <Card className="mx-4">
    <CardBody className="p-4">
      <h1>Forgot Password</h1>
      <p className="text-muted">Forgot your account password</p>
      <PasswordForm.Forgot onSubmit={fields => actions.forgotPassword(fields)} />
    </CardBody>
  </Card>
);

const enhance = compose<any, any>(connectUser);

export default enhance(ForgotPassword);
