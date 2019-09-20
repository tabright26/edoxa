import React from "react";
import { Button } from "reactstrap";

const CloseButton = ({ className, onClick, width = "75px" }) => (
  <Button className={className} style={{ width }} color="primary" size="sm" type="button" onClick={onClick}>
    Close
  </Button>
);

export default CloseButton;
