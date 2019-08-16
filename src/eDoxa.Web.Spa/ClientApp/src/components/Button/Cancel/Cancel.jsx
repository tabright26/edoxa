import React from "react";
import { Button } from "reactstrap";

const CancelButton = ({ className, onClick }) => (
  <Button className={className} color="primary" outline size="sm" type="button" onClick={onClick}>
    Cancel
  </Button>
);

export default CancelButton;
