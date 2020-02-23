import React, { FunctionComponent } from "react";
import { faTwitter } from "@fortawesome/free-brands-svg-icons";
import { REACT_APP_TWITTER_URL } from "keys";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { SizeProp } from "@fortawesome/fontawesome-svg-core";

type Props = {
  size?: SizeProp;
};

export const Twitter: FunctionComponent<Props> = ({ size, children }) => (
  <a href={REACT_APP_TWITTER_URL} target="_blank" rel="noopener noreferrer">
    {children ? children : <FontAwesomeIcon icon={faTwitter} size={size} />}
  </a>
);
