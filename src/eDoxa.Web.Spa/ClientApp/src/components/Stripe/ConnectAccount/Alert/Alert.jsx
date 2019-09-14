import React from "react";
import { Alert, Button } from "reactstrap";

const StripeConnectAccountAlert = () => (
  <Alert color="primary" className="mb-0">
    <div>Your account is not verified!</div>
    <div className="d-flex">
      <p className="mb-0">Your account is currently not verified. This may prevent you from accessing certain features of this website...</p>
      <Button color="primary" size="sm" className="mt-auto ml-auto text-white">
        Verify
      </Button>
    </div>
  </Alert>
);

export default StripeConnectAccountAlert;
