import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";

const SubmitButton: FunctionComponent<any> = ({ className, children, type = "submit", color = "primary", width = "75px", ...attributes }) => (
  <Button size="sm" className={className} style={{ width }} color={color} {...attributes} type={type}>
    {children}
  </Button>
);

export default SubmitButton;
