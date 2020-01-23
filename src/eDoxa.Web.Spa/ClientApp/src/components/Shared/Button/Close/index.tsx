import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";

export const Close: FunctionComponent<any> = ({
  className,
  onClick,
  width = "75px"
}) => (
  <Button
    className={className}
    style={{ width }}
    color="primary"
    type="button"
    onClick={onClick}
  >
    Close
  </Button>
);
