import React from "react";
import { Button } from "reactstrap";

const CloseButton = ({ className, onClick }) => (
  <Button className={className} color="primary" size="sm" type="button" onClick={onClick}>
    Close
  </Button>
);

export default CloseButton;
