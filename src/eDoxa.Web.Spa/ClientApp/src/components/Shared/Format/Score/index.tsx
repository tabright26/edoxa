import React, { FunctionComponent } from "react";
import ReactCurrencyFormat from "react-currency-format";

interface Props {
  score: number;
  decimals?: number;
  bold?: boolean;
}

export const Score: FunctionComponent<Props> = ({
  score,
  decimals = 2,
  bold
}) => {
  if (score) {
    const fixed = score.toFixed(decimals);
    if (bold) {
      return (
        <strong>
          <ReactCurrencyFormat
            value={fixed}
            displayType="text"
            thousandSeparator
            decimalScale={2}
          />
        </strong>
      );
    }
    return (
      <span>
        <ReactCurrencyFormat
          value={fixed}
          displayType="text"
          thousandSeparator
          decimalScale={2}
          fixedDecimalScale
        />
      </span>
    );
  }
  return <span>--</span>;
};
