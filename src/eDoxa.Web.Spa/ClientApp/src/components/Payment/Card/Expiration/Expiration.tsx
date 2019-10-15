import React, { Fragment } from "react";
import CardExpirationMonth from "./Month";
import CardExpirationYear from "./Year";

interface CardExpirationProps {
  month: Number;
  year: Number;
  className?: string;
}

const CardExpiration = ({ month, year, className }: CardExpirationProps) => (
  <Fragment>
    <CardExpirationMonth month={month} className={className} />
    <span className={className}>/</span>
    <CardExpirationYear year={year} className={className} />
  </Fragment>
);

export default CardExpiration;
