import React from "react";
import { Button } from "reactstrap";

const YesButton = ({ className, onClick }) => (
  <Button className={className} color="primary" size="sm" type="button" onClick={onClick}>
    Yes
  </Button>
);

export default YesButton;
