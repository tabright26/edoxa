import React, { FunctionComponent } from "react";

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
      return <strong>{fixed}</strong>;
    }
    return <span>{fixed}</span>;
  }
  return <span>--</span>;
};
