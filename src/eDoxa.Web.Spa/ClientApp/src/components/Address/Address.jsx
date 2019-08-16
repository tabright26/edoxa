import React from "react";

const Address = ({ address }) => {
  const Country = React.lazy(() => import(`./Country/${address.country}`));
  return <Country {...address} />;
};

export default Address;
