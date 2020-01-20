import React, { FunctionComponent } from "react";
import { Month } from "./Month";
import { Year } from "./Year";

interface Props {
  month: number;
  year: number;
  className?: string;
}

export const Exp: FunctionComponent<Props> = ({
  month,
  year,
  className = null
}) => (
  <>
    <Month month={month} className={className} />
    <span className={className}>/</span>
    <Year year={year} className={className} />
  </>
);
