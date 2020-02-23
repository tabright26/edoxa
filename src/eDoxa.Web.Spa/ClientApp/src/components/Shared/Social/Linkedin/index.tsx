import React, { FunctionComponent } from "react";
import { faLinkedin } from "@fortawesome/free-brands-svg-icons";
import { REACT_APP_LINKEDIN_URL } from "keys";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { SizeProp } from "@fortawesome/fontawesome-svg-core";

type Props = {
  size?: SizeProp;
};

export const Linkedin: FunctionComponent<Props> = ({ size, children }) => (
  <a href={REACT_APP_LINKEDIN_URL} target="_blank" rel="noopener noreferrer">
    {children ? children : <FontAwesomeIcon icon={faLinkedin} size={size} />}
  </a>
);
