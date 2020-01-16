import React, { Fragment } from "react";
import { Input, FormFeedback, Label } from "reactstrap";

const TextInput = ({
  formGroup: FormGroup = Fragment,
  label = null,
  input = null,
  placeholder = null,
  disabled,
  meta = null,
  ...attributes
}) => (
  <FormGroup>
    {label && <Label>{label}</Label>}
    <Input
      {...input}
      {...attributes}
      bsSize="sm"
      disabled={disabled}
      placeholder={placeholder}
      invalid={!disabled && meta.touched && !!meta.error}
    />
    {meta && <FormFeedback>{meta.error}</FormFeedback>}
  </FormGroup>
);

export default TextInput;
