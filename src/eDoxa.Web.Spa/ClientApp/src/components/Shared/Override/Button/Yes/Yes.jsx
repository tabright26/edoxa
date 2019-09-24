import React from "react";
import { Button } from "reactstrap";

const YesButton = ({ type = "button" | "submit", className, onClick = null, width = "75px" }) => (
  <Button className={className} color="primary" size="sm" type={type} onClick={onClick} style={{ width }}>
    Yes
  </Button>
);

export default YesButton;
