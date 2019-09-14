import React from "react";
import { Input } from "reactstrap";

const SelectInput = ({ children, input, label, type = "select", meta: { touched, error }, ...attributes }) => (
  <Input {...input} {...attributes} placeholder={label} type={type} invalid={touched && error}>
    {children}
  </Input>
);

export default SelectInput;
