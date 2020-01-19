import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";

export const No: FunctionComponent<any> = ({
  className,
  onClick,
  width = "75px"
}) => (
  <Button
    className={className}
    color="secondary"
    size="sm"
    type="button"
    onClick={onClick}
    style={{ width }}
  >
    No
  </Button>
);
