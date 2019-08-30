import React from "react";
import { Alert } from "reactstrap";

const SuccessAlert = ({ heading, body, footer = null }) => (
  <Alert color="primary">
    <h4 className="alert-heading">{heading}</h4>
    <p className={footer ? null : "mb-0"}>{body}</p>
    {footer ? (
      <>
        <hr />
        <p className="mb-0">{footer}</p>
      </>
    ) : null}
  </Alert>
);

export default SuccessAlert;
