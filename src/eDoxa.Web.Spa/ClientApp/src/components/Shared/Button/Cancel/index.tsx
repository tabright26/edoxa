import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";

export const Cancel: FunctionComponent<any> = ({
  className,
  onClick,
  width = "75px"
}) => (
  <Button
    className={className}
    style={{ width }}
    color="primary"
    outline
    size="sm"
    type="button"
    onClick={onClick}
  >
    Cancel
  </Button>
);
