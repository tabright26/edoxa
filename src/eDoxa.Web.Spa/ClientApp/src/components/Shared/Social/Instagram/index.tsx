import React, { FunctionComponent } from "react";
import { faInstagram } from "@fortawesome/free-brands-svg-icons";
import { REACT_APP_INSTAGRAM_URL } from "keys";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { SizeProp } from "@fortawesome/fontawesome-svg-core";

type Props = {
  size?: SizeProp;
};

export const Instagram: FunctionComponent<Props> = ({ size, children }) => (
  <a href={REACT_APP_INSTAGRAM_URL} target="_blank" rel="noopener noreferrer">
    {children ? children : <FontAwesomeIcon icon={faInstagram} size={size} />}
  </a>
);
