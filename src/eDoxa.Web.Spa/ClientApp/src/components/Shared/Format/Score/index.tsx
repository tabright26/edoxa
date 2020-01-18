import React, { FunctionComponent } from "react";

interface Props {
  score: number;
  decimals?: number;
}

export const Score: FunctionComponent<Props> = ({ score, decimals = 2 }) => {
  if (!score) {
    return <span>--</span>;
  }
  return <span>{score.toFixed(decimals)}</span>;
};
