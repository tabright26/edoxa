import React, { FunctionComponent, MouseEventHandler } from "react";
import { Button } from "reactstrap";

type TypeProp = "button" | "submit";

interface YesButtonProps {
  type: TypeProp;
  className?: string;
  onClick?: MouseEventHandler;
  width?: string;
}

export const Yes: FunctionComponent<YesButtonProps> = ({
  type,
  className,
  onClick = null,
  width = "75px"
}) => (
  <Button
    className={className}
    color="primary"
    size="sm"
    type={type}
    onClick={onClick}
    style={{ width }}
  >
    Yes
  </Button>
);
