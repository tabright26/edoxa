import React, { FunctionComponent } from "react";
import { REACT_APP_SUPPORT_EMAILADDRESS } from "keys";

export const EmailAddress: FunctionComponent = () => (
  <a href={`mailto:${REACT_APP_SUPPORT_EMAILADDRESS}`}>
    {REACT_APP_SUPPORT_EMAILADDRESS}
  </a>
);
