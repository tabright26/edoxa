import React, { FunctionComponent } from "react";
import { Alert } from "reactstrap";

interface Props {
  error: string;
  anyTouched: boolean;
}

export const ValidationSummary: FunctionComponent<Props> = ({
  error,
  anyTouched
}) =>
  error && anyTouched ? (
    <Alert color="primary">
      <strong>Error:</strong> {error}
    </Alert>
  ) : null;
