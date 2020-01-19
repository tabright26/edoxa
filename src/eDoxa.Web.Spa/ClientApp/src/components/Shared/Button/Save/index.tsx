import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";

export const Save: FunctionComponent<any> = ({ className, width = "75px" }) => (
  <Button
    className={className}
    style={{ width }}
    color="primary"
    size="sm"
    type="submit"
  >
    Save
  </Button>
);
