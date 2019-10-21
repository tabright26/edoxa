import React, { Fragment } from "react";
import { Input, FormFeedback } from "reactstrap";

const TextInput = ({ formGroup: FormGroup = Fragment, input = null, label = null, disabled, meta = null, ...attributes }) => (
  <FormGroup>
    <Input bsSize="sm" {...input} {...attributes} disabled={disabled} placeholder={label} invalid={!disabled && meta.touched && meta.error} />
    {meta ? <FormFeedback>{meta.error}</FormFeedback> : null}
  </FormGroup>
);

export default TextInput;
