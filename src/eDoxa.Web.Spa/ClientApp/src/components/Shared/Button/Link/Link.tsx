import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { IconDefinition } from "@fortawesome/free-solid-svg-icons";
import { sentenceCase } from "change-case";
import { LinkContainer } from "react-router-bootstrap";

const style = {
  textDecoration: "none"
};

interface Props {
  to?: string;
  className?: string;
  children: any;
  disabled?: boolean;
  icon?: IconDefinition;
  onClick?: () => void;
}

const LinkButton: FunctionComponent<Props> = ({
  className,
  children,
  onClick = null,
  icon = null,
  disabled,
  to = null
}) => {
  if (to && !onClick) {
    return (
      <LinkContainer to={to}>
        <Button
          style={style}
          size="sm"
          color="link"
          disabled={disabled}
          className={className}
        >
          {icon && <FontAwesomeIcon icon={icon} />} {sentenceCase(children)}
        </Button>
      </LinkContainer>
    );
  } else {
    return (
      <Button
        style={style}
        size="sm"
        color="link"
        disabled={disabled}
        className={className}
        onClick={onClick}
      >
        <small className="text-uppercase">
          {icon && <FontAwesomeIcon icon={icon} />} {children}
        </small>
      </Button>
    );
  }
};

export default LinkButton;
