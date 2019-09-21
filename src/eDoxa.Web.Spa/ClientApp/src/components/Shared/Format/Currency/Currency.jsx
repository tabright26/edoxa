import React from "react";
import Currency from "react-currency-format";
import Icon from "components/Shared/Icon";
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
              <Icon.Money className="text-primary my-auto" />
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
              <Icon.Token className="text-primary my-auto" />
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
