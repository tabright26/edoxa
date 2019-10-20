import React, { FunctionComponent } from "react";
import { Badge } from "reactstrap";

interface VerificationBadgeProps {
  className: string;
  verified: boolean;
}

const VerificationBadge: FunctionComponent<VerificationBadgeProps> = ({ className, verified }) => {
  if (verified) {
    return (
      <Badge pill className={`px-2 ${className}`} color="success">
        <span>verified</span>
      </Badge>
    );
  } else {
    return (
      <Badge pill className={`px-2 ${className}`} color="secondary">
        <span>unverified</span>
      </Badge>
    );
  }
};

export default VerificationBadge;
