import React from "react";
import { Input } from "reactstrap";

const SelectInput = ({ children, input, label, type, meta: { touched, error }, ...attributes }) => (
  <Input {...input} {...attributes} placeholder={label} type={type} bsSize="sm" invalid={touched && error}>
    {children}
  </Input>
);

export default SelectInput;
