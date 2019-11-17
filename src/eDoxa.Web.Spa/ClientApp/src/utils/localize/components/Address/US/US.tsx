import React, { FunctionComponent } from "react";

const US: FunctionComponent<any> = ({ name, line1, line2, city, state, postalCode }) => (
  <address className="text-uppercase m-0">
    <span>{line1}</span>
    <br />
    {line2 ? (
      <>
        <span>{line2}</span>
        <br />
      </>
    ) : null}
    <span>
      {city}, {state ? <>{state}</> : null} {postalCode}
    </span>
    <br />
    <span>{name}</span>
  </address>
);

export default US;
