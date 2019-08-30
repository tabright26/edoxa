import React from "react";
import { Button } from "reactstrap";

const SubmitButton = ({ className, children, type = "submit", color = "primary", ...attributes }) => (
  <Button className={className} color={color} {...attributes} type={type}>
    {children}
  </Button>
);

export default SubmitButton;
