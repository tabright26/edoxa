import React, { FunctionComponent } from "react";
import { Input } from "reactstrap";

export const Select: FunctionComponent<any> = ({
  children,
  input,
  label,
  size = "sm",
  meta: { touched, error },
  ...props
}) => (
  <Input
    {...input}
    {...props}
    bsSize={size}
    placeholder={label}
    invalid={touched && !!error}
  >
    {children}
  </Input>
);
