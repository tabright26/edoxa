import React from "react";
import { Alert } from "reactstrap";

const ValidationSummary = ({ error }) => (
  <Alert color="primary">
    <strong>Error:</strong> {error}
  </Alert>
);

export default ValidationSummary;
