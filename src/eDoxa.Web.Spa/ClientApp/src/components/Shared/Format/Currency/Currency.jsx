import React from "react";
import Currency from "react-currency-format";
import MoneyIcon from "icons/Money";
import TokenIcon from "icons/Token";
import UnknownCurrency from "./Unknown";

const CurrencyFormat = ({ currency, amount = 0, justify = false }) => {
  switch (currency ? currency.toLowerCase() : null) {
    case "money":
      return (
        <Currency
          value={amount}
          displayType="text"
          thousandSeparator
          decimalScale={2}
          fixedDecimalScale={true}
          renderText={value => (
            <div className="d-flex">
              <MoneyIcon className="text-primary my-auto" />
              <span className={justify ? "ml-auto" : null}>{value}</span>
            </div>
          )}
        />
      );
    case "token":
      return (
        <Currency
          value={amount}
          displayType="text"
          thousandSeparator
          renderText={value => (
            <div className="d-flex">
              <TokenIcon className="text-primary my-auto" />
              <span className={justify ? "ml-auto" : null}>{amount}</span>
            </div>
          )}
        />
      );
    default:
      return <UnknownCurrency />;
  }
};

export default CurrencyFormat;
