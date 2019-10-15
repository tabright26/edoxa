import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";

const CloseButton: FunctionComponent<any> = ({ className, onClick, width = "75px" }) => (
  <Button className={className} style={{ width }} color="primary" size="sm" type="button" onClick={onClick}>
    Close
  </Button>
);

export default CloseButton;
