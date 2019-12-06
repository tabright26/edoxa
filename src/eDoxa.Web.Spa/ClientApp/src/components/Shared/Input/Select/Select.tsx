import React, { FunctionComponent } from "react";
import { Input } from "reactstrap";

const SelectInput: FunctionComponent<any> = ({
  children,
  input,
  label,
  type = "select",
  meta: { touched, error },
  ...attributes
}) => (
  <Input
    {...attributes}
    {...input}
    bsSize="sm"
    placeholder={label}
    type={type}
    invalid={touched && !!error}
  >
    {children}
  </Input>
);

export default SelectInput;
