import React from "react";

const PayoutBucketFormat = ({ prevSize, nextSize }) => <span>{prevSize !== nextSize ? `${prevSize}-${nextSize}` : `${nextSize}`}</span>;

export default PayoutBucketFormat;
