import React, { FunctionComponent } from "react";

interface Props {
  month: number;
  className?: string;
}

export const Month: FunctionComponent<Props> = ({
  month,
  className = null
}) => <span className={className}>{month.toString().padStart(2, "0")}</span>;
