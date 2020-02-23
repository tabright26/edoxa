import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";

interface Props {
  className?: string;
  size?: string;
  onClick?: () => void;
}

export const Cancel: FunctionComponent<Props> = ({
  className = null,
  onClick = null,
  size = null
}) => (
  <Button
    className={className}
    color="primary"
    size={size}
    outline
    onClick={onClick}
  >
    Cancel
  </Button>
);
