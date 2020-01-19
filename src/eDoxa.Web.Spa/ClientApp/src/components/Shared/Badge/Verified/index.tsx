import React, { FunctionComponent } from "react";
import { Badge } from "reactstrap";

interface Props {
  className?: string;
  verified: boolean;
}

export const Verified: FunctionComponent<Props> = ({
  className = null,
  verified
}) =>
  verified ? (
    <Badge pill className={`px-2 ${className}`} color="primary">
      <span>Verified</span>
    </Badge>
  ) : (
    <Badge pill className={`px-2 ${className}`} color="secondary">
      <span>Unverified</span>
    </Badge>
  );
