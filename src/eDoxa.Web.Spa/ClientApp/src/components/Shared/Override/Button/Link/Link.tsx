import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { IconDefinition } from "@fortawesome/free-solid-svg-icons";

const style = {
  textDecoration: "none"
};

interface LinkButtonProps {
  className?: string;
  children: any;
  disabled?: boolean;
  icon: IconDefinition;
  onClick: () => void;
}

const LinkButton: FunctionComponent<LinkButtonProps> = ({ className, children, onClick, icon, disabled }) => (
  <Button style={style} size="sm" color="link" disabled={disabled} className={className} onClick={onClick}>
    <small className="text-uppercase">
      <FontAwesomeIcon icon={icon} /> {children}
    </small>
  </Button>
);

export default LinkButton;
