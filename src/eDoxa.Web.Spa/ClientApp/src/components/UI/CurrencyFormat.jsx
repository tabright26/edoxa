import React from 'react';
import CurrencyFormat from 'react-currency-format';

const Currency = ({ currency, amount }) => {
  const normalizedCurrency = currency.toLowerCase();
  if (!amount && amount !== 0) {
    return '--';
  }
  if (normalizedCurrency === 'money') {
    return (
      <CurrencyFormat
        value={amount}
        displayType="text"
        thousandSeparator
        prefix="$"
      />
    );
  }
  return <CurrencyFormat value={amount} displayType="text" />;
};

export default Currency;
