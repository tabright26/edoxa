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
    className={className}
    style={style}
    color={color}
    {...attributes}
    type={type}
  >
    {children}
  </Button>
);
