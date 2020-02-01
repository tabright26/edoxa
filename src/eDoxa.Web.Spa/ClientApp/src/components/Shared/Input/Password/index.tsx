import React, { FunctionComponent, Fragment } from "react";
import { Input, Label, FormFeedback } from "reactstrap";

export const Password: FunctionComponent<any> = ({
  children,
  input,
  label = null,
  placeholder = null,
  size = "sm",
  disabled = false,
  meta,
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
      invalid={!disabled && meta.touched && !!meta.error}
    >
      {children}
    </Input>
    {meta && <FormFeedback>{meta.error}</FormFeedback>}
  </FormGroup>
);
