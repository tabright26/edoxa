import React from "react";
import Currency from "react-currency-format";

const CurrencyFormat = ({ currency, amount }) => {
  if (!currency) {
    return "--";
  }
  if (!amount && amount !== 0) {
    return 0;
  }
  const normalizedCurrency = currency.toLowerCase();
  if (normalizedCurrency === "money") {
    return <Currency value={amount} displayType="text" thousandSeparator prefix="$" />;
  }
  return <Currency value={amount} displayType="text" />;
};

export default CurrencyFormat;
