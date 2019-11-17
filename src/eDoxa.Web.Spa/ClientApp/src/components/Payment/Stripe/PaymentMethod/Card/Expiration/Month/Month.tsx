import React from "react";

interface CardExpirationMonthProps {
  month: number;
  className?: string;
}

const CardExpirationMonth = ({ month, className }: CardExpirationMonthProps) => <span className={className}>{month.toString().padStart(2, "0")}</span>;

export default CardExpirationMonth;
