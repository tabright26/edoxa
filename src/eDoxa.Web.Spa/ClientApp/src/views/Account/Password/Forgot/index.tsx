import React, { FunctionComponent } from "react";
import { Card, CardBody } from "reactstrap";
import PasswordForm from "components/Service/Identity/Account/Password/Form";

const Forgot: FunctionComponent = () => (
  <Card className="mx-4">
    <CardBody className="p-4">
      <h1>Forgot password</h1>
      <p className="text-muted">
        Enter your email address. You will receive a link to reset your
        password.
      </p>
      <PasswordForm.Forgot />
    </CardBody>
  </Card>
);

export default Forgot;
