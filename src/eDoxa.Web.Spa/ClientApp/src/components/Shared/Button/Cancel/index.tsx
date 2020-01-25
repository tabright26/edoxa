import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";

export const Cancel: FunctionComponent<any> = ({
  className,
  onClick,
  width = "75px",
  size = "sm"
}) => (
  <Button
    className={className}
    style={{ width }}
    color="primary"
    outline
    size={size}
    type="button"
    onClick={onClick}
  >
    Cancel
  </Button>
);
