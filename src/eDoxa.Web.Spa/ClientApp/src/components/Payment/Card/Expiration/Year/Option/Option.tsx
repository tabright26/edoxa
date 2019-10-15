import React from "react";

interface CardExpirationYearOptionProps {
  year: Number;
}

const CardExpirationYearOption = ({ year }: CardExpirationYearOptionProps) => <option value={year.toString()}>{year.toString().substring(2)}</option>;

export default CardExpirationYearOption;
