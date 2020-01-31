import React, { FunctionComponent } from "react";
import ReactCurrencyFormat from "react-currency-format";
import {
  Currency as CurrencyDto,
  CURRENCY_TYPE_MONEY,
  CURRENCY_TYPE_TOKEN
} from "types";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faDollarSign } from "@fortawesome/free-solid-svg-icons";
import { faGg } from "@fortawesome/free-brands-svg-icons";

interface Props {
  currency?: CurrencyDto;
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
  alignment = "right"
}) => {
  switch (currency ? currency.type.toLowerCase() : null) {
    case CURRENCY_TYPE_MONEY: {
      return (
        <ReactCurrencyFormat
          value={currency.amount}
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
    case CURRENCY_TYPE_TOKEN: {
      return (
        <ReactCurrencyFormat
          value={currency.amount}
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
