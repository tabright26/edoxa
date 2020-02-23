import React, { FunctionComponent } from "react";
import { faFacebook } from "@fortawesome/free-brands-svg-icons";
import { REACT_APP_FACEBOOK_URL } from "keys";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { SizeProp } from "@fortawesome/fontawesome-svg-core";

type Props = {
  size?: SizeProp;
};

export const Facebook: FunctionComponent<Props> = ({ size, children }) => (
  <a href={REACT_APP_FACEBOOK_URL} target="_blank" rel="noopener noreferrer">
    {children ? children : <FontAwesomeIcon icon={faFacebook} size={size} />}
  </a>
);
