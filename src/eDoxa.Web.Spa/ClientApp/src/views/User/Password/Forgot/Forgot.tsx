import React, { FunctionComponent } from "react";
import { Card, CardBody } from "reactstrap";
import UserPasswordForm from "components/User/Password/Form";

const ForgotUserPassword: FunctionComponent<any> = () => (
  <Card className="mx-4">
    <CardBody className="p-4">
      <h1>Forgot Password</h1>
      <p className="text-muted">Forgot your account password</p>
      <UserPasswordForm.Forgot />
    </CardBody>
  </Card>
);

export default ForgotUserPassword;
