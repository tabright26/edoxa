import React, { FunctionComponent } from "react";
import { countries } from "utils/localize/countries";

const Address: FunctionComponent<any> = ({ address }) => {
  console.log(address);
  const Country = React.lazy(() =>
    import(`./${address.country.toUpperCase()}`)
  );
  return (
    <Country
      name={
        countries.find(
          country =>
            country.twoDigitIso.toUpperCase() === address.country.toUpperCase()
        ).name
      }
      {...address}
    />
  );
};

export default Address;
