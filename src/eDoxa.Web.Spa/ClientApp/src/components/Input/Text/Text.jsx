import React from "react";
import { Input, FormFeedback } from "reactstrap";

const TextInput = ({ input, label, meta: { touched, error }, ...attributes }) => (
  <>
    <Input bsSize="sm" {...input} {...attributes} placeholder={label} invalid={touched && error} />
    <FormFeedback>{error}</FormFeedback>
  </>
);

export default TextInput;
