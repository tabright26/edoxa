import React from "react";
import { Input, FormFeedback } from "reactstrap";

const TextInput = ({ input, label, type = "text", meta: { touched, error }, ...attributes }) => (
  <>
    <Input {...input} {...attributes} placeholder={label} type={type} invalid={touched && error} />
    <FormFeedback>{error}</FormFeedback>
  </>
);

export default TextInput;
