import React, { FunctionComponent } from "react";
import { Card, CardBody } from "reactstrap";
import PasswordForm from "components/User/Password/Form";

const ResetPassword: FunctionComponent = () => (
  <Card className="mx-4">
    <CardBody className="p-4">
      <h1 className="mb-3">Reset password</h1>
      <PasswordForm.Reset />
    </CardBody>
  </Card>
);

export default ResetPassword;
