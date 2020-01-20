import React, { FunctionComponent } from "react";

interface Props {
  year: number;
  className?: string;
}

export const Year: FunctionComponent<Props> = ({ year, className }) => (
  <span className={className}>{year.toString().substring(2)}</span>
);
