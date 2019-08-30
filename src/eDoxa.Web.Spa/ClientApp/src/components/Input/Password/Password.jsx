import React from "react";
import { Input } from "reactstrap";

const PasswordInput = ({ children, input, label, type = "password", meta: { touched, error }, ...attributes }) => (
  <Input {...input} {...attributes} placeholder={label} type={type} invalid={touched && error}>
    {children}
  </Input>
);

export default PasswordInput;
