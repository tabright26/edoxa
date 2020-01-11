import React, { FunctionComponent } from "react";
import { Badge } from "reactstrap";

interface VerificationBadgeProps {
  className: string;
  verified: boolean;
}

const VerificationBadge: FunctionComponent<VerificationBadgeProps> = ({
  className,
  verified
}) => {
  if (verified) {
    return (
      <Badge pill className={`px-2 ${className}`} color="primary">
        <span>Verified</span>
      </Badge>
    );
  } else {
    return (
      <Badge pill className={`px-2 ${className}`} color="secondary">
        <span>Unverified</span>
      </Badge>
    );
  }
};

export default VerificationBadge;
