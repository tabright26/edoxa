import React from "react";

interface CardExpirationMonthOptionProps {
  month: Number;
}

const CardExpirationMonthOption = ({ month }: CardExpirationMonthOptionProps) => <option value={month.toString()}>{month.toString().padStart(2, "0")}</option>;

export default CardExpirationMonthOption;
