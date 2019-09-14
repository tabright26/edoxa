import React from "react";
import { Button } from "reactstrap";

const YesButton = ({ type = "button" | "submit", className, onClick = null }) => (
  <Button className={className} color="primary" size="sm" type={type} onClick={onClick}>
    Yes
  </Button>
);

export default YesButton;
