import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";

const NoButton: FunctionComponent<any> = ({ className, onClick, width = "75px" }) => (
  <Button className={className} color="secondary" size="sm" type="button" onClick={onClick} style={{ width }}>
    No
  </Button>
);

export default NoButton;
