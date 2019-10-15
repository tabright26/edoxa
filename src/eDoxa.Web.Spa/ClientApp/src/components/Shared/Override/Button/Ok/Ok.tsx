import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";

const OkButton: FunctionComponent<any> = ({ className, onClick }) => (
  <Button className={className} color="primary" size="sm" type="button" onClick={onClick}>
    Ok
  </Button>
);

export default OkButton;
