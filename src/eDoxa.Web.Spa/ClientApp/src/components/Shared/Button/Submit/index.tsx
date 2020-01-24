import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";

export const Submit: FunctionComponent<any> = ({
  className,
  children,
  type = "submit",
  color = "primary",
  style,
  ...attributes
}) => (
  <Button
    {...attributes}
    className={className}
    style={style}
    color={color}
    type={type}
  >
    {children}
  </Button>
);