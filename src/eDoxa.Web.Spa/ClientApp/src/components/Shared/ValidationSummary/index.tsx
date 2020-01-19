import React, { FunctionComponent } from "react";
import { Alert } from "reactstrap";

interface Props {
  error: string;
}

export const ValidationSummary: FunctionComponent<Props> = ({ error }) =>
  error ? (
    <Alert color="primary">
      <strong>Error:</strong> {error}
    </Alert>
  ) : null;
