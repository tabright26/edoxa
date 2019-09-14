import React from "react";

const ScoreFormat = ({ score, decimals = 2 }) => {
  if (!score) {
    return <span>--</span>;
  }
  return <span>{score.toFixed(decimals)}</span>;
};

export default ScoreFormat;
