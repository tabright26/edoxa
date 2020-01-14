import React, { Fragment } from "react";
import { Input, FormFeedback } from "reactstrap";

const TextInput = ({
  formGroup: FormGroup = Fragment,
  input = null,
  label = null,
  disabled,
  meta = null,
  ...attributes
}) => (
  <FormGroup>
    <Input
      {...input}
      {...attributes}
      bsSize="sm"
      disabled={disabled}
      placeholder={label}
      invalid={!disabled && meta.touched && !!meta.error}
    />
    {meta && <FormFeedback>{meta.error}</FormFeedback>}
  </FormGroup>
);

export default TextInput;
