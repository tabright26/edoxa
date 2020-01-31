import React, { FunctionComponent } from "react";
import { Button } from "reactstrap";
import LaddaButton, { SLIDE_RIGHT } from "react-ladda";

interface Props {
  loading?: boolean;
  className?: string;
  color?: string;
  size?: string;
  block?: boolean;
  disabled?: boolean;
  outline?: boolean;
  onClick?: () => void;
}

export const Submit: FunctionComponent<Props> = ({
  children,
  className = null,
  loading = null,
  color = "primary",
  size = null,
  block = null,
  disabled = null,
  outline = null,
  onClick = null
}) =>
  loading ? (
    <LaddaButton
      className={`btn ${color && "btn-" + color} ${size &&
        "btn-" + size} btn-ladda ${block && "btn-block"} ${className}`}
      loading={loading}
      data-style={SLIDE_RIGHT}
      data-spinner-lines={12}
    >
      {children}
    </LaddaButton>
  ) : (
    <Button
      className={className}
      color={color}
      type="submit"
      size={size}
      block={block}
      disabled={disabled}
      outline={outline}
      onClick={onClick}
    >
      {children}
    </Button>
  );
