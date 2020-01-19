import React, { FunctionComponent } from "react";

interface Props {
  prevSize: number;
  nextSize: number;
}

export const PayoutBucket: FunctionComponent<Props> = ({
  prevSize,
  nextSize
}) => (
  <span>
    {prevSize !== nextSize ? `${prevSize}-${nextSize}` : `${nextSize}`}
  </span>
);
