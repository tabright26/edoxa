import React, { FunctionComponent } from "react";
import { Alert } from "reactstrap";

interface Props {
  error: string | string[];
  anyTouched: boolean;
}

export const ValidationSummary: FunctionComponent<Props> = ({
  error,
  anyTouched
}) => {
  if (error && anyTouched) {
    if (Array.isArray(error)) {
      return (
        <Alert color="primary">
          <ul className="mb-0">
            {error.map((message, index) => (
              <li key={index}>{message}</li>
            ))}
          </ul>
        </Alert>
      );
    } else {
      return <Alert color="primary">{error}</Alert>;
    }
  }
  return null;
};
