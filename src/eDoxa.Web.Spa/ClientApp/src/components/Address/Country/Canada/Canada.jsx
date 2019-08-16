import React from "react";

const Canada = ({ line1, line2, city, country, state, postalCode }) => (
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
    <span>{country}</span>
  </address>
);

export default Canada;
