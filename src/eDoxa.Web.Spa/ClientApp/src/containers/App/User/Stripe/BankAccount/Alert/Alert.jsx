import React from "react";
import { Alert, Button } from "react-bootstrap";

import withStripeBankAccountHoc from "../withStripeBankAccountHoc";

const StripeBankAccountAlert = ({ hasBankAccount }) => {
  if (hasBankAccount) {
    return (
      <Alert variant="success" className="mt-4 mb-0">
        <Alert.Heading>Bank Account</Alert.Heading>
        <div className="d-flex">
          <p className="mb-0">To activate cash withdrawal, you must add your bank account information. The funds will then be transferred directly to your account from eDoxa.</p>
          <Button variant="danger" size="sm" className="my-auto mt-auto">
            Remove
          </Button>
        </div>
      </Alert>
    );
  } else {
    return (
      <Alert variant="info" className="mt-3 mb-0">
        <Alert.Heading>Bank Account</Alert.Heading>
        <div className="d-flex">
          <p className="mb-0">To activate cash withdrawal, you must add your bank account information. The funds will then be transferred directly to your account from eDoxa.</p>
          <Button variant="info" size="sm" className="mt-auto ml-auto">
            Link
          </Button>
        </div>
      </Alert>
    );
  }
};

export default withStripeBankAccountHoc(StripeBankAccountAlert);