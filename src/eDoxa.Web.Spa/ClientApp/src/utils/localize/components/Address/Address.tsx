import React, { FunctionComponent } from "react";
import { countries } from "utils/localize/countries";

const Address: FunctionComponent<any> = ({ address }) => {
  const Country = React.lazy(() => import(`./${address.country}`));
  return <Country name={countries.find(country => country.twoDigitIso === address.country).name} {...address} />;
};

export default Address;