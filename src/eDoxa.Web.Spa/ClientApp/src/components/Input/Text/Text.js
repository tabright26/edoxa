import React from "react";
import { Input, FormFeedback } from "reactstrap";

const TextInput = ({ input, label, type, meta: { touched, error } }) => (
  <>
    <Input {...input} placeholder={label} type={type} bsSize="sm" invalid={touched && error} />
    <FormFeedback>{error}</FormFeedback>
  </>
);

export default TextInput;
