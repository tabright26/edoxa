import React, { FunctionComponent } from "react";

interface Props {
  readonly className?: string;
}

export const ModalSubtitle: FunctionComponent<Props> = ({
  className,
  children
}) => (
  <small className={`d-block mt-2 text-muted ${className}`}>{children}</small>
);
