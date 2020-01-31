import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { IconProp } from "@fortawesome/fontawesome-svg-core";

interface Props {
  className?: string;
  disabled?: boolean;
  icon?: IconProp;
  size?: "sm" | "lg";
  uppercase?: boolean;
  onClick?: () => void;
}

export const Link: FunctionComponent<Props> = ({
  className = null,
  size = null,
  disabled = null,
  uppercase = null,
  onClick = null,
  icon,
  children
}) => (
  <Button
    className={className}
    color="link"
    size={size}
    disabled={disabled}
    onClick={onClick}
  >
    <small className={`${uppercase && "text-uppercase"}`}>
      {icon && <FontAwesomeIcon icon={icon} />} {children}
    </small>
  </Button>
);
