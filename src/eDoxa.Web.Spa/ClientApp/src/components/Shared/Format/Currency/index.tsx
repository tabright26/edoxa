import React, { FunctionComponent } from "react";
import ReactCurrencyFormat from "react-currency-format";
import { Currency as CurrencyType } from "types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faDollarSign } from "@fortawesome/free-solid-svg-icons";
import { faGg } from "@fortawesome/free-brands-svg-icons";

interface Props {
  currency: CurrencyType;
  amount: number;
  alignment?: "right" | "left" | "center" | "justify";
}

interface CurrencyIconProps {
  icon: any;
  amount: number;
  alignment?: "right" | "left" | "center" | "justify";
}

const CurrencyIconFormat: FunctionComponent<CurrencyIconProps> = ({
  icon: Icon,
  amount,
  alignment
}) => {
  switch (alignment) {
    case "justify": {
      return (
        <div className="d-flex">
          <Icon className="text-primary my-auto" />
          <span className="ml-auto">{amount}</span>
        </div>
      );
    }
    case "right": {
      return (
        <div className="text-right">
          <Icon className="text-primary my-auto" />
          <span>{amount}</span>
        </div>
      );
    }
    case "left": {
      return (
        <div className="text-left">
          <Icon className="text-primary my-auto" />
          <span>{amount}</span>
        </div>
      );
    }
    case "center": {
      return (
        <div className="text-center">
          <Icon className="text-primary my-auto" />
          <span>{amount}</span>
        </div>
      );
    }
  }
};

export const Currency: FunctionComponent<Props> = ({
  currency,
  amount = 0,
  alignment = "right"
}) => {
  switch (currency ? currency.toLowerCase() : null) {
    case "money": {
      return (
        <ReactCurrencyFormat
          value={amount}
          displayType="text"
          thousandSeparator
          decimalScale={2}
          fixedDecimalScale={true}
          renderText={(value: number) => (
            <CurrencyIconFormat
              icon={props => <FontAwesomeIcon icon={faDollarSign} {...props} />}
              amount={value}
              alignment={alignment}
            />
          )}
        />
      );
    }
    case "token": {
      return (
        <ReactCurrencyFormat
          value={amount}
          displayType="text"
          thousandSeparator
          renderText={(value: number) => (
            <CurrencyIconFormat
              icon={props => <FontAwesomeIcon icon={faGg} {...props} />}
              amount={value}
              alignment={alignment}
            />
          )}
        />
      );
    }
    default: {
      return <span>n/a</span>;
    }
  }
};
