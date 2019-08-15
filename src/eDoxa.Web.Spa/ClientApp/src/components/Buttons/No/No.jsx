import React from "react";
import { Button } from "reactstrap";

const NoButton = ({ className, onClick }) => (
  <Button className={className} color="secondary" size="sm" type="button" onClick={onClick}>
    No
  </Button>
);

export default NoButton;
