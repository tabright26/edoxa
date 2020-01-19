import React, { FunctionComponent, Fragment } from "react";
import { Input, FormFeedback, Label } from "reactstrap";

export const Text: FunctionComponent<any> = ({
  input = null,
  label = null,
  placeholder = null,
  disabled,
  size = "sm",
  meta = null,
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
    />
    {meta && <FormFeedback>{meta.error}</FormFeedback>}
  </FormGroup>
);
