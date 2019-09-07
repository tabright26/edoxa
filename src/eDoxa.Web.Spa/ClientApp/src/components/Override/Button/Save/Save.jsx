import React from "react";
import { Button } from "reactstrap";

const SaveButton = ({ className, width = "75px" }) => (
  <Button className={className} style={{ width }} color="primary" size="sm" type="submit">
    Save
  </Button>
);

export default SaveButton;
