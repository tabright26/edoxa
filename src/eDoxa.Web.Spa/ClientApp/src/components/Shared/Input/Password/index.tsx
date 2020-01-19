import React, { FunctionComponent, Fragment } from "react";
import { Input, Label } from "reactstrap";

export const Password: FunctionComponent<any> = ({
  children,
  input,
  label = null,
  placeholder = null,
  size = "sm",
  disabled = false,
  meta: { touched, error },
  formGroup: FormGroup = Fragment,
  ...props
}) => (
  <FormGroup>
    {label && <Label>{label}</Label>}
    <Input
      {...input}
      {...props}
      placeholder={placeholder}
      bsSize={size}
      disabled={disabled}
      invalid={touched && !!error}
    >
      {children}
    </Input>
  </FormGroup>
);
