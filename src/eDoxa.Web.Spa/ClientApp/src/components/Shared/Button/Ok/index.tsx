import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";

export const Ok: FunctionComponent<any> = ({ className, onClick }) => (
  <Button
    className={className}
    color="primary"
    size="sm"
    type="button"
    onClick={onClick}
  >
    Ok
  </Button>
);
