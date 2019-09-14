import React from "react";
import { Alert, Button } from "react-bootstrap";

const StripeConnectAccountAlert = () => (
  <Alert variant="primary" className="mb-0">
    <Alert.Heading>Your account is not verified!</Alert.Heading>
    <div className="d-flex">
      <p className="mb-0">Your account is currently not verified. This may prevent you from accessing certain features of this website...</p>
      <Button variant="primary" size="sm" className="mt-auto ml-auto text-white">
        Verify
      </Button>
    </div>
  </Alert>
);

export default StripeConnectAccountAlert;
