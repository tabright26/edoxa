import React from 'react';

const BucketFormat = ({ prevSize, nextSize }) => (
  <span>
    {prevSize !== nextSize ? `${prevSize}-${nextSize}` : `${nextSize}`}
  </span>
);

export default BucketFormat;
