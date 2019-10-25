import React from "react";

interface CardExpirationYearProps {
  year: number;
  className?: string;
}

const CardExpirationYear = ({ year, className }: CardExpirationYearProps) => <span className={className}>{year.toString().substring(2)}</span>;

export default CardExpirationYear;
