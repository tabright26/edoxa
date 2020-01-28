import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";

interface Props {
  className?: string;
  onClick?: () => void;
}

export const Cancel: FunctionComponent<Props> = ({
  className = null,
  onClick = null
}) => (
  <Button
    className={className}
    color="primary"
    size="sm"
    outline
    onClick={onClick}
  >
    Cancel
  </Button>
);
