import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";

const CancelButton: FunctionComponent<any> = ({ className, onClick, width = "75px" }) => (
  <Button className={className} style={{ width }} color="primary" outline size="sm" type="button" onClick={onClick}>
    Cancel
  </Button>
);

export default CancelButton;
