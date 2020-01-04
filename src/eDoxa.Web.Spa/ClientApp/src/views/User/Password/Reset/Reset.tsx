import React, { FunctionComponent } from "react";
import { Card, CardBody } from "reactstrap";
import PasswordForm from "components/User/Password/Form";

const ResetPassword: FunctionComponent = () => (
  <Card className="mx-4">
    <CardBody className="p-4">
      <h1>Reset Password</h1>
      <p className="text-muted">Reset your account password</p>
      <PasswordForm.Reset />
    </CardBody>
  </Card>
);

export default ResetPassword;
