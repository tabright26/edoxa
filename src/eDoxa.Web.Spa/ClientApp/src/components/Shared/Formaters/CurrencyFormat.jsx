import React from 'react';
import CurrencyFormat from 'react-currency-format';

const Currency = ({ currency, amount }) => {
  if (!currency) {
    return '--';
  }
  if (!amount && amount !== 0) {
    return 0;
  }
  const normalizedCurrency = currency.toLowerCase();
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
