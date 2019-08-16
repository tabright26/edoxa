import React from "react";
import { Button } from "reactstrap";

const SaveButton = ({ className }) => (
  <Button className={className} color="primary" size="sm" type="submit">
    Save
  </Button>
);

export default SaveButton;
